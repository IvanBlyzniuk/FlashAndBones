using App;
using App.Systems.Wave;
using App.World.Shop;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

namespace App.Systems.GameStates
{
    public class FightingState : IState
    {
        private WaveSystem waveSystem;
        private GameStatesSystem gameStatesSystem;
        private Light2D globalLight;
        private float transitDuration = 2.5f;
        private float musicFadeTime = 1f;
        private float elapseTime;
        private AudioClip fightingMusic;
        private AudioSource audioSource;
        private GameObject shop;
        List<WeaponShopItem> weaponShops;
        List<UpgradeShopItem> upgradeShops;
        public FightingState(GameStatesSystem gameStatesSystem, WaveSystem waveSystem, Light2D globalLight, AudioClip fightingMusic, AudioSource audioSource,GameObject shop)
        {
            this.gameStatesSystem = gameStatesSystem;
            this.waveSystem = waveSystem;
            this.globalLight = globalLight;
            this.fightingMusic = fightingMusic;
            this.audioSource = audioSource;
            this.shop = shop;
            weaponShops = new List<WeaponShopItem>(shop.GetComponentsInChildren<WeaponShopItem>());
            upgradeShops = new List<UpgradeShopItem>(shop.GetComponentsInChildren<UpgradeShopItem>());
        }
        public void Enter()
        {
            waveSystem.StartWave();
            gameStatesSystem.StartCoroutine(StartMusic());
            foreach (var weaponShop in weaponShops)
            {
                weaponShop.SetRandomWeapon();
            }
            foreach (var upgradeShop in upgradeShops)
            {
                upgradeShop.SetRandomUpgrade();
            }
        }
        public void Exit()
        {
            elapseTime = 0;
            gameStatesSystem.StartCoroutine(StopMusic());
        }

        public void Update()
        {
            if (globalLight.intensity > 0.5f)
            {
                elapseTime += Time.deltaTime;
                globalLight.intensity = Mathf.Lerp(1f, 0.5f, elapseTime / transitDuration);
            }

        }
        private IEnumerator StartMusic()
        {
            yield return new WaitForSeconds(musicFadeTime);
            audioSource.volume = 0f;
            audioSource.clip = fightingMusic;
            audioSource.Play();
            float currentTime = 0;
            float start = audioSource.volume;
            while (currentTime < musicFadeTime)
            {
                currentTime += Time.deltaTime;
                audioSource.volume = Mathf.Lerp(start, 0.1f, currentTime / musicFadeTime);
                yield return null;
            }
            yield break;
        }
        private IEnumerator StopMusic()
        {
            float currentTime = 0;
            float start = audioSource.volume;
            while (currentTime < musicFadeTime)
            {
                currentTime += Time.deltaTime;
                audioSource.volume = Mathf.Lerp(start, 0, currentTime/ musicFadeTime);
                yield return null;
            }
            audioSource.Stop();
            yield break;
        }
    }
}

