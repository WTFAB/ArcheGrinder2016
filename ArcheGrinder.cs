using System;
using System.Windows.Forms;
using System.Threading;
using ArcheBuddy.Bot.Classes;

namespace ArcheGrinder
{
    public class ArcheGrinder : Core
    {
        Thread formThread;
        FormMain form;
        private bool shouldShutdown = false;
        public bool isFormOpen = true;

        public static string GetPluginAuthor()
        {
            return "Taranira";
        }

        public static string GetPluginVersion()
        {
            return "ArcheGrinder BETA 0.0.0.5";
            
        }

        public static string GetPluginDescription()
        {
            return "Mob grinder with Auroria/Library/Hasla Features";
        }

        public void loadForm()
        {
            try
            {
                Application.Run(form);
            }
            catch (Exception error)
            {
                Log(error.ToString());
            }
        }


        //Call this on plugin start
        public void RunForm()
        {
            ClearLogs();

            if (gameState != GameState.Ingame)
            {
                Log("Waiting to be ingame to fully load the plugin...", System.Drawing.Color.Orange);
                while (gameState != GameState.Ingame)
                    Thread.Sleep(50);
            }

            isFormOpen = true;
            form = new FormMain(this);
            form.SetCore(this);
            formThread = new Thread(loadForm);
            formThread.SetApartmentState(ApartmentState.STA);
            formThread.Start();
            Log("Interface loaded", System.Drawing.Color.Green);

            while (isFormOpen && me != null)
            {
                Thread.Sleep(100);
            }

            PluginStop();
        }

        public void PluginStop()
        {
            CancelMoveTo();
            CancelSkill();
            CancelTarget();

            if (formThread.IsAlive)
                formThread.Abort();

            Log("ArcheGrinder closed");
        }

        public void PluginRun()
        {
            ClearLogs();

            if (gameState != GameState.Ingame)
            {
                Log("Waiting to be ingame to fully load the plugin...", System.Drawing.Color.Orange);
                while (gameState != GameState.Ingame)
                    Thread.Sleep(50);
            }
            
            isFormOpen = true;
            form = new FormMain(this);
            form.SetCore(this);
            formThread = new Thread(loadForm);
            formThread.SetApartmentState(ApartmentState.STA);
            formThread.Start();
            Log("Interface loaded", System.Drawing.Color.Green);
            
            while (isFormOpen && me != null)
            {
                Thread.Sleep(100);
            }

            PluginStop();
        }   
    }
}