using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class PlayerInstaller : MonoInstaller
{
        public override void InstallBindings()
        {
            Container.Bind<JumpAction>().AsSingle();
            Container.Bind<AttackAction>().AsSingle();
            Container.Bind<UseItemAction>().AsSingle();
            Container.Bind<IInventoryService>().To<InventoryService>().AsSingle();
            Container.Bind<PlayerController>().AsSingle().NonLazy();
        }
}

