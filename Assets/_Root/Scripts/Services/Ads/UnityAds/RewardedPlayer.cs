using System;
using UnityEngine;
using UnityEngine.Advertisements;

namespace Services.Ads.UnityAds
{
    internal sealed class RewardedPlayer : UnityAdsPlayer, IDisposable
    {
        public RewardedPlayer(string id) : base(id)
        {
            Skipped += OnSkipping;
            Finished += OnFinishing;
        }

        protected override void OnPlaying() => Advertisement.Show(Id);
        protected override void Load() => Advertisement.Load(Id);

        public void Dispose()
        {
            Skipped -= OnSkipping;
            Finished -= OnFinishing;
        }

        private void OnSkipping() => Debug.Log("Skipped! No reward for you!");

        private void OnFinishing() => Debug.Log("You watched the whole thing! It was like 3 hours!");




    }
}
