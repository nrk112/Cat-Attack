using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinalProject.Interfaces
{
    interface IInteractiveObject
    {
        int GetHits();
        int GetScorePerHit();
        BaseClasses.GameObject.State GetState();
    }
}
