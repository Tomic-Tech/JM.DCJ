using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using JM.Core;
using JM.Vehicles;
using JM.DCJ.Vehicle;

namespace JM.DCJ.WinUI
{
    abstract class MikuniPresenter : MenuPresenter
    {
        protected Diag.MikuniOptions options;
        public MikuniPresenter()
        {
            options = new Diag.MikuniOptions();
            options.Parity = Diag.MikuniParity.None;
            MenuItemInitialize();
            FunctionInitialize();
        }

        protected abstract void MenuItemInitialize();

        protected virtual void FunctionInitialize()
        {
            FunctionSelected = new Dictionary<string, ProtocolFunc>();
            FunctionSelected[Resources.READ_CURRENT_TROUBLE_CODE] = () => 
            {
                DlgPresenter.ShowStatus(Resources.COMMUNICATING);
                List<TroubleCode> codes = null;
                Task.Factory.StartNew(() => 
                {
                    Mikuni protocol = new Mikuni(Resources.Commbox, options);
                    codes = protocol.ReadCurrentTroubleCode();
                }).ContinueWith((t) => 
                {
                    if (t.IsFaulted)
                    {
                        DlgPresenter.ShowFatalBox(t.Exception.InnerException.Message, null);
                    }
                    else
                    {
                        if (this is HJ125T_16APresenter)
                        {
                            Resources.ChangeTroubleCodeFor16A(codes);
                        }
                        else
                        {
                            Resources.ChangeTroubleCodeForNone16A(codes);
                        }
                        TCPresenter.TroubleCodeList = codes;
                        TCPresenter.Show();
                        DlgPresenter.Hide();
                    }
                });
            };

            FunctionSelected[Resources.READ_HISTORY_TROUBLE_CODE] = () => 
            {
                DlgPresenter.ShowStatus(Resources.COMMUNICATING);
                List<TroubleCode> codes = null;

                Task.Factory.StartNew(() =>
                {
                    Mikuni protocol = new Mikuni(Resources.Commbox, options);
                    codes = protocol.ReadHistoryTroubleCode();
                }).ContinueWith((t) => 
                {
                    if (t.IsFaulted)
                    {
                        DlgPresenter.ShowFatalBox(t.Exception.InnerException.Message, null);
                    }
                    else
                    {
                        if (this is HJ125T_16APresenter)
                        {
                            Resources.ChangeTroubleCodeFor16A(codes);
                        }
                        else
                        {
                            Resources.ChangeTroubleCodeForNone16A(codes);
                        }
                        TCPresenter.TroubleCodeList = codes;
                        TCPresenter.Show();
                        DlgPresenter.Hide();
                    }
                });
            };

            FunctionSelected[Resources.READ_DATA_STREAM] = () => 
            {
                DlgPresenter.ShowStatus(Resources.COMMUNICATING);
                Resources.LiveDataVector = Database.GetLiveData("Mikuni");
                DSPresenter.Items = Resources.LiveDataVector.Items;
                Mikuni protocol = null;

                DSPresenter.ProtocolTask = Task.Factory.StartNew(() => 
                {
                    protocol = new Mikuni(Resources.Commbox, options);
                    DSPresenter.Protocol = protocol;
                    protocol.ReadDataStream(Resources.LiveDataVector);
                });

                DSPresenter.Show();
                DlgPresenter.Hide();

                DSPresenter.ProtocolTask.ContinueWith((t) => 
                {
                    if (t.IsFaulted)
                    {
                        if (DSPresenter.Protocol != null)
                        {
                            DSPresenter.Protocol.StopReadDataStream();
                            DlgPresenter.ShowFatalBox(t.Exception.InnerException.Message, (sender, e) => 
                            {
                                DSPresenter.Back();
                            });
                        }
                    }
                });
            };

            FunctionSelected[Resources.READ_ECU_VERSION] = () =>
            {
                DlgPresenter.ShowStatus(Resources.COMMUNICATING);
                Mikuni.ChineseVersion version = new Mikuni.ChineseVersion();
                Task.Factory.StartNew(() =>
                {
                    Mikuni protocol = new Mikuni(Resources.Commbox, options);
                    string hex = protocol.GetECUVersion();
                    version = Mikuni.FormatECUVersionForChina(hex);
                }).ContinueWith((t) => 
                {
                    if (t.IsFaulted)
                    {
                        DlgPresenter.ShowFatalBox(t.Exception.InnerException.Message, null);
                    }
                    else
                    {
                        string text = version.Hardware + "\nV" + version.Software;
                        DlgPresenter.ShowFatalBox(text, null);
                    }
                });
            };

            FunctionSelected[Resources.TPS_IDLE_SETTING] = () => 
            {
                DlgPresenter.ShowStatus(Resources.COMMUNICATING);

                Task.Factory.StartNew(() => 
                {
                    Mikuni protocol = new Mikuni(Resources.Commbox, options);
                    protocol.TPSIdleSetting();
                }).ContinueWith((t) => 
                {
                    if (t.IsFaulted)
                    {
                        DlgPresenter.ShowFatalBox(t.Exception.InnerException.Message, null);
                    }
                    else
                    {
                        DlgPresenter.ShowFatalBox(Resources.TPS_IDLE_SETTING_SUCCESS, null);
                    }
                });
            };

            FunctionSelected[Resources.LONG_TERM_LEARN_VALUE_ZONE_INITIALIZATION] = () => 
            {
                DlgPresenter.ShowStatus(Resources.COMMUNICATING);

                Task.Factory.StartNew(() => 
                {
                    Mikuni protocol = new Mikuni(Resources.Commbox, options);
                    protocol.LongTermLearnValueZoneInitialization();
                }).ContinueWith((t) => 
                {
                    if (t.IsFaulted)
                    {
                        DlgPresenter.ShowFatalBox(t.Exception.InnerException.Message, null);
                    }
                    else
                    {
                        DlgPresenter.ShowFatalBox(Resources.LONG_TERM_LEARN_VALUE_ZONE_INITIALIZATION_SUCCESS, null);
                    }
                });
            };

            FunctionSelected[Resources.ISC_LEARN_VALUE_INITIALIZATION] = () => 
            {
                DlgPresenter.ShowStatus(Resources.COMMUNICATING);

                Task.Factory.StartNew(() =>
                {
                    Mikuni protocol = new Mikuni(Resources.Commbox, options);
                    protocol.ISCLearnValueInitialize();
                }).ContinueWith((t) => 
                {
                    if (t.IsFaulted)
                    {
                        DlgPresenter.ShowFatalBox(t.Exception.InnerException.Message, null);
                    }
                    else
                    {
                        DlgPresenter.ShowFatalBox(Resources.ISC_LEARN_VALUE_INITIALIZATION_SUCCESS, null);
                    }
                });
            };
        }
    }
}
