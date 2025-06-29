using Assets.Scripts.Application.UseCases;
using Assets.Scripts.Presentation.Bridges;
using Assets.Scripts.Presentation.Controllers;
using Assets.Scripts.Presentation.Interfaces;
using Assets.Scripts.Presentation.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zenject;

namespace Assets.Scripts.Infrastructure.Installers
{
    public class MenuLoginRegisterInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {

            Container.Bind<LoginUseCase>().AsTransient();
            Container.Bind<RegisterUseCase>().AsTransient();

            Container.Bind<IPresenter>()
                     .To<LoginRegisterUIController>()
                     .FromComponentInHierarchy()
                     .AsSingle();

            Container.Bind<LoginController>().AsTransient();
            Container.Bind<RegisterController>().AsTransient();

            //Container.Bind<LoginBridge>() no estaba originalmente
            //    .FromComponentInHierarchy()
            //    .AsSingle();
        }
    }
}