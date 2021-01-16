using IPlusApp.Models;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Printing;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls.Primitives;
using System.Windows.Forms;
using System.Windows.Shapes;
using System.IO;

namespace IPlusApp.Functionality
{
    public class ShowSessionFunctionality
    {
        private readonly CommonFunctionality _commonFunctionality = new CommonFunctionality();
        private string selectedSessionPath = System.IO.Path.Combine(
Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData),
"SelectedSession.txt");

        #region HelperMethodPanelCheckBox
        public List<CheckBox> GetCheckBoxSessions(List<SessionStepOneModel> models, bool isSetColorToWhite = false)
        {
            var checkBoxList = new List<CheckBox>();
            var sessionCount = 1;
            foreach (var item in models)
            {
                var newCheckBox = new CheckBox();
                newCheckBox.Name = item.Year + "/" + item.Month + "/" + item.Day + "/" + item.Hour + "/" + item.Minutes;
                newCheckBox.Text = "  Session " + sessionCount + "       " + item.StrarTime + "       " + item.EndTime;
                newCheckBox.Width = 410;
                if (!isSetColorToWhite)
                    newCheckBox.BackColor = Color.FromArgb(238, 242, 243);
                if (isSetColorToWhite)
                    newCheckBox.BackColor = Color.FromArgb(255, 255, 255);
                checkBoxList.Add(newCheckBox);
                sessionCount++;
            }
            return checkBoxList;

        }

        public Panel GetPanel(int y)
        {
            var panel = new Panel();
            panel.Location = new Point(7, y);
            return panel;
        }
        public Label GetLabel(string lblText, int yLocation = 8)
        {
            var label = new Label();
            label.RightToLeft = RightToLeft.Yes;
            label.Text = lblText;
            label.Location = new Point(2, yLocation);
            label.Font = new Font("Tahoma", 7f, FontStyle.Bold);
            return label;
        }
        #endregion

        #region HelperMethodPanelCalender
        public TabPage GetTabPage(List<SessionStepOneModel> sessionList, string tabPageText)
        {

            var newPage = new TabPage(tabPageText);
            var checkBoxes = GetCheckBoxSessions(sessionList, true);
            var yLocation = 7;
            foreach (var checkBox in checkBoxes)
            {
                checkBox.Location = new Point(7, yLocation);
                newPage.Controls.Add(checkBox);
                yLocation = yLocation + 23;
                yLocation = yLocation + 10;
                var lineLabel = GetLabel("_", yLocation);
                yLocation = yLocation + 4;
                lineLabel.Width = 520;
                lineLabel.Height = 1;
                lineLabel.BackColor = Color.FromArgb(213, 230, 236);
                newPage.Controls.Add(lineLabel);
            }
            return newPage;
        }
        private void CheckBox_Checked(object sender, EventArgs e, Panel PanelCheckBoxList)
        {
            CheckBox chk = (sender as CheckBox);
            if (chk.Checked)
            {
                foreach (Control c in PanelCheckBoxList.Controls)
                {
                    if (c is CheckBox)
                    {
                        CheckBox cb = (CheckBox)c;
                        var checkBoxNameMustChecked = cb.Name.Split('/')[0] + "/" + cb.Name.Split('/')[1] + "/" + cb.Name.Split('/')[2];
                        if (cb.Checked == false && chk.Name == checkBoxNameMustChecked)
                        {
                            cb.Checked = true;
                        }
                    }
                }
            }
            else
            {
                foreach (Control c in PanelCheckBoxList.Controls)
                {
                    if (c is CheckBox)
                    {
                        CheckBox cb = (CheckBox)c;
                        var checkBoxNameMustChecked = cb.Name.Split('/')[0] + "/" + cb.Name.Split('/')[1] + "/" + cb.Name.Split('/')[2];
                        if (cb.Checked == true && chk.Name == checkBoxNameMustChecked)
                        {
                            cb.Checked = false;
                        }
                    }
                }
            }
        }
        public void AddCheckBoxToTabPage(List<SessionStepOneModel> sessionList, TabPage tabPage, Panel PanelCheckBoxList)
        {
            var sessionDays = sessionList.GroupBy(x => x.Day).ToList();
            var yLocation = 7;
            foreach (var session in sessionDays)
            {
                var checkBox = new CheckBox();
                checkBox.CheckedChanged += new EventHandler((sender, e) => CheckBox_Checked(sender, e, PanelCheckBoxList));
                var sessionMonthText = _commonFunctionality.GetMonth(session.Select(x => x.Month).First());
                var sessionYear = session.Select(x => x.Year).First();
                var sessionMonth = session.Select(x => x.Month).First();
                checkBox.Name = sessionYear + "/" + sessionMonth + "/" + session.Key;
                checkBox.Width = 520;
                checkBox.Text = session.Count() + "  " + "session " + "  " + sessionMonthText + " " + session.Key;
                checkBox.Location = new Point(7, yLocation);
                tabPage.Controls.Add(checkBox);
                yLocation = yLocation + 23;
                yLocation = yLocation + 10;
                var lineLabel = GetLabel("_", yLocation);
                yLocation = yLocation + 4;
                lineLabel.Width = 520;
                lineLabel.Height = 1;
                lineLabel.BackColor = Color.FromArgb(213, 230, 236);

                tabPage.Controls.Add(lineLabel);
            }

        }
        public void AddButtonToTabPage(List<SessionStepOneModel> sessionList, TabPage tabPage)
        {
            var sessionDays = sessionList.GroupBy(x => x.Day).ToList();
            var btnXLocation = 6;
            var btnYLocation = 29;
            var checkBoxXLocation = 65;
            var checkBoxYLocation = 75;
            var btnCount = 0;
            foreach (var session in sessionDays)
            {
                var btn = new Button();
                btn.Size = new Size(87, 70);
                if (btnCount == 5)
                {
                    btnCount = 0;
                    btnYLocation = btnYLocation + 76;
                    checkBoxYLocation = checkBoxYLocation + 74;
                }
                btn.Location = new Point(btnXLocation, btnYLocation);
                btn.BackColor = Color.FromArgb(238, 242, 243);
                btn.Text = session.Key + "\n\n" + session.Count() + "  " + "session ";
                btn.TextAlign = ContentAlignment.TopLeft;
                var checkBox = new CheckBox();
                checkBox.Text = " ";
                checkBox.Location = new Point(checkBoxXLocation, checkBoxYLocation);
                tabPage.Controls.Add(btn);
                btnXLocation = btnXLocation + 89;
                checkBoxXLocation = checkBoxXLocation + 89;
                //tabPage.Controls.Add(checkBox);
                btnCount++;
            }
        }
        public void AddCheckBoxtoTabPage(List<SessionStepOneModel> sessionList, TabPage tabPage)
        {
            var sessionDays = sessionList.GroupBy(x => x.Day).ToList();
        }
        #endregion

        #region DrawIForm
        public void DrawPanelCheckBox(Panel panelContainer, List<SessionStepOneModel> sessionList)
        {
            var sessionGroup = sessionList.GroupBy(x => x.Day).ToList();
            var yLocation = 29;
            var isFirstLabel = true;
            foreach (var sessions in sessionGroup)
            {
                var lblText = sessions.Key
                    + " " + _commonFunctionality.GetMonth(sessions.Select(x => x.Month).First()) + " "
                    + sessions.Select(x => x.Year).First();
                //var year = sessions.Select(x => x.Year).First();
                //var month = sessions.Select(x => x.Month).First();
                //var day = sessions.Select(x => x.Day).First();
                //var lblText = _commonFunctionality.ToGregorianDate(year, month, day);
                if (isFirstLabel)
                {
                    var panelLabel = GetLabel(lblText);
                    panelContainer.Controls.Add(panelLabel);
                    isFirstLabel = false;
                }
                else
                {
                    var lineLabel = GetLabel("_______________________________________________________________________________________________________", yLocation);
                    yLocation = yLocation + 10;
                    lineLabel.Width = 520;
                    lineLabel.Height = 3;
                    lineLabel.BackColor = Color.FromArgb(213, 230, 236);
                    panelContainer.Controls.Add(lineLabel);
                    var panelLabel = GetLabel(lblText, yLocation);
                    panelContainer.Controls.Add(panelLabel);
                    yLocation = yLocation + 18;
                }
                var checkBoxes = GetCheckBoxSessions(sessions.ToList());
                foreach (var checkBox in checkBoxes)
                {
                    checkBox.Location = new Point(7, yLocation);
                    panelContainer.Controls.Add(checkBox);
                    yLocation = yLocation + 23;
                    if (checkBoxes.Last() != checkBox)
                    {
                        yLocation = yLocation + 10;
                        var lineLabel = GetLabel("_____________________________________________________________________________________________________", yLocation);
                        yLocation = yLocation + 4;
                        lineLabel.Width = 520;
                        lineLabel.Height = 1;
                        lineLabel.BackColor = Color.FromArgb(213, 230, 236);
                        panelContainer.Controls.Add(lineLabel);
                    }

                }
            }
        }

        public void DrawPanelCalender(Panel panelCalender, List<SessionStepOneModel> sessionList)
        {
            var sessionGroup = sessionList.GroupBy(x => x.Month);
            var tabControl = new TabControl();
            //tabControl.BackColor = Color.FromArgb(255, 255, 255);
            tabControl.Size = new Size(511, 406);
            //tabControl.AutoScrollOffset = true;
            //tabControl.Dock = DockStyle.Fill;
            foreach (var sessions in sessionGroup)
            {
                var month = sessions.Select(x => x.Month).FirstOrDefault();
                var year = sessions.Select(x => x.Year).FirstOrDefault();
                var monthLabel = _commonFunctionality.GetMonth(month);
                var tabText = " " + year + " " + monthLabel + " ";
                var newPage = GetTabPage(sessions.ToList(), tabText);
                newPage.BackColor = Color.FromArgb(255, 255, 255);
                newPage.AutoScroll = true;
                tabControl.TabPages.Add(newPage);
            }
            panelCalender.Controls.Add(tabControl);
        }

        public void DrawPanelCalenderBtn(Panel panelCalender, List<SessionStepOneModel> sessionList, Panel PanelCheckBoxList)
        {
            var sessionGroup = sessionList.GroupBy(x => x.Month).ToList();
            var tabControl = new TabControl();
            tabControl.Anchor = (AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right);
            tabControl.Size = new Size(511, 406);
            foreach (var sessions in sessionGroup)
            {
                var month = sessions.Select(x => x.Month).FirstOrDefault();
                var year = sessions.Select(x => x.Year).FirstOrDefault();
                var monthLabel = _commonFunctionality.GetMonth(month);
                var tabText = " " + year + " " + monthLabel + " ";
                var newPage = new TabPage();
                newPage.Text = " " + tabText + " ";
                AddCheckBoxToTabPage(sessions.ToList(), newPage, PanelCheckBoxList);
                newPage.BackColor = Color.FromArgb(255, 255, 255);
                newPage.AutoScroll = true;
                tabControl.TabPages.Add(newPage);
            }
            panelCalender.Controls.Add(tabControl);
        }

        public void DrawPanelCheckBoxList(Panel panel, List<SessionStepOneModel> sessionList)
        {
            var sessionGroupYear = sessionList.GroupBy(x => x.Year).OrderBy(x => x.Key).ToList();
            var yLocation = 29;
            var isFirstLabel = true;
            foreach (var sesstionYear in sessionGroupYear)
            {
                var sessionGroupMonth = sesstionYear.GroupBy(x => x.Month).OrderBy(x => x.Key).ToList();
                isFirstLabel = true;
                foreach (var sessionMonth in sessionGroupMonth)
                {
                    var sessionGroupDay = sessionMonth.GroupBy(x => x.Day).ToList();
                    //isFirstLabel = true;
                    foreach (var sessions in sessionGroupDay)
                    {
                        var lblText = sessions.Key
                    + " " + _commonFunctionality.GetMonth(sessions.Select(x => x.Month).First()) + " "
                    + sessions.Select(x => x.Year).First();
                        //var year = sessions.Select(x => x.Year).First();
                        //var month = sessions.Select(x => x.Month).First();
                        //var day = sessions.Select(x => x.Day).First();
                        //var lblText = _commonFunctionality.ToGregorianDate(year, month, day);
                        if (isFirstLabel)
                        {
                            var panelLabel = GetLabel(lblText);
                            panel.Controls.Add(panelLabel);
                            isFirstLabel = false;
                        }
                        else
                        {
                            var lineLabel = GetLabel("_", yLocation);
                            yLocation = yLocation + 10;
                            lineLabel.Width = 520;
                            lineLabel.Height = 3;
                            lineLabel.BackColor = Color.FromArgb(213, 230, 236);
                            panel.Controls.Add(lineLabel);
                            var panelLabel = GetLabel(lblText, yLocation);
                            panel.Controls.Add(panelLabel);
                            yLocation = yLocation + 18;
                        }
                        var checkBoxes = GetCheckBoxSessions(sessions.ToList());
                        foreach (var checkBox in checkBoxes)
                        {
                            checkBox.Location = new Point(7, yLocation);
                            panel.Controls.Add(checkBox);
                            yLocation = yLocation + 23;
                            if (checkBoxes.Last() != checkBox)
                            {
                                yLocation = yLocation + 10;
                                var lineLabel = GetLabel("_", yLocation);
                                yLocation = yLocation + 4;
                                lineLabel.Width = 520;
                                lineLabel.Height = 1;
                                lineLabel.BackColor = Color.FromArgb(213, 230, 236);
                                panel.Controls.Add(lineLabel);
                            }

                        }
                    }
                }
            }
        }
        #endregion

        #region WriteSelectedSession
        public void WriteSelectedSessions(List<string> sessionsName)
        {
            var sessions = _commonFunctionality.ReadSessionStepOneModels();
            var selectedSessions = sessions.Where(x => sessionsName.Any(y => y == x.Name)).ToList();
            _commonFunctionality.WriteToFile(selectedSessionPath, selectedSessions);
        }
        #endregion

    }
}
