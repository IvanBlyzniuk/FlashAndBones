namespace App.Effects
{
    public interface IEffectHolder
    {
        public void EnableEffect(BaseStatusEffect effect);
        public void UpdateEffect(BaseStatusEffect effect);
        public void DisableEffect(BaseStatusEffect effect);
    }
}
