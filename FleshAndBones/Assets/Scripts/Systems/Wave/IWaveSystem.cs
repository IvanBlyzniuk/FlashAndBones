using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace App.Systems.Wave
{
    public interface IWaveSystem
    {
        public void ReportKilled(string enemyType);
    }
}
