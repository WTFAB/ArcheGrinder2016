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
            return "WTFAB";
        }

        public static string GetPluginVersion()
        {
            return "ArcheGrinder[Alpha] 1.0.1.2";

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
            form.FormClosed += form_FormClosed;
            formThread = new Thread(loadForm);
           // formThread.SetApartmentState(ApartmentState.STA);
            formThread.Start();
            Log("Interface loaded", System.Drawing.Color.Green);

            while (isFormOpen)
                if (me == null)
                {
                    PluginStop();
                    {
                        Thread.Sleep(100);
                    }


                }
        }
        public void PluginStop()
        {
            CancelMoveTo();
            CancelSkill();


            if (form != null)
            {
                Log("ArcheGrinder plugin is succesfully stopped", System.Drawing.Color.Green); //changed Color
                form.Invoke(new Action(() => form.Close()));
                form.Invoke(new Action(() => form.Dispose()));

            }
            Application.Exit();

            formThread.Abort();
            /*
            if (formThread.IsAlive)
                formThread.Abort();

            Log("ArcheGrinder closed");
            */
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
            form.FormClosed += form_FormClosed;
            formThread = new Thread(loadForm);
            formThread.SetApartmentState(ApartmentState.STA);
            formThread.Start();
            Log("Interface loaded", System.Drawing.Color.Green);

            while (isFormOpen)
                if (me == null)
                {
                    PluginStop();
                }


            Thread.Sleep(100);
        }
        void form_FormClosed(object sender, FormClosedEventArgs e)
        {
            Log("Form closed - stopping the plugin", System.Drawing.Color.Red); //changed Color
            isFormOpen = false;
            if (isPluginRun(pluginPath))
            {
                PluginStop();

            }



        }
    }
}