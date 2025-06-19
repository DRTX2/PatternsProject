using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts.Presentation.Interfaces
{
    public interface IPresenter
    {
        void ShowErrors(List<string> errors);
        void HideErrors();
    }
}
