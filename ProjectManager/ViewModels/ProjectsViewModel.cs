using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DotVVM.Framework.ViewModel;
using Newtonsoft.Json;
using ProjectManager.Models;
using System.Web;

namespace ProjectManager.ViewModels
{
    public class ProjectsViewModel : BaseViewModel
    {
        public string AddedProjectTitle { get; set; }

        public List<ProjectData> Projects { get; set; } = new List<ProjectData>();

        public void AddProject()
        {
            Projects.Add(new ProjectData() { Title = AddedProjectTitle });
            AddedProjectTitle = "";
            string projectDataJson = JsonConvert.SerializeObject(Projects, Formatting.Indented);
            System.IO.File.WriteAllText(path, projectDataJson);
        }

        public override Task PreRender()
        {
            var dataFileProjects = System.IO.File.ReadAllText(path);
            Projects = JsonConvert.DeserializeObject<List<ProjectData>>(dataFileProjects);
            return base.PreRender();
        }

        public void CompleteProject(ProjectData project)
        {
            project.IsCompleted = true;
        }

        public void RedirectToTasks(Guid Id)
        {
            Context.RedirectToRoute("Tasks", new { Id = Id });
        }
    }
}

