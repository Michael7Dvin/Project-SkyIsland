﻿using Cysharp.Threading.Tasks;
using Infrastructure.Progress;

namespace Infrastructure.SceneServices.ProgressServices
{
    public interface ILevelProgressService
    {
        AllProgress CurrentProgress { get; }
        void SetCurrentProgress(AllProgress progress);

        UniTask Save();
        void Load();
    }
}