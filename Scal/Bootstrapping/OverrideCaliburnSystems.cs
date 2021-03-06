using System;
using Scal.Services;
using Scal.ViewLocation;
using StructureMap;

namespace Scal.Bootstrapping
{
    public class OverrideCaliburnSystems : IStartupTask
    {
        private readonly ViewLocationManagement _mgm;
        private readonly AppModel _model;
        private readonly IContainer _container;

        public OverrideCaliburnSystems(ViewLocationManagement mgm, AppModel model, IContainer container)
        {
            _mgm = mgm;
            _model = model;
            _container = container;
        }

        public void Run()
        {
            _mgm.Add(_model.GetViewLocators(_container));
            _mgm.Add(new CaliburnMicroLocator());
            _mgm.Activate();
            _container.GetInstance<ValueConverterManagement>(); //Instantiation is enough
        }

        public TaskPriority Priority
        {
            get { return TaskPriority.Earliest; }
        }
    }
}