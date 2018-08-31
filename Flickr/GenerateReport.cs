using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Report
{
    public class GenerateReport : Base
    {
        public void GenerateReports(int iStep, string iTestNum, bool iResult, string iMessage = "-")
        {
            iMessage = iMessage.Replace("<", "&lt;").Replace(">", "&lt");
            iMessage = iMessage.Replace("\n", "/br");
            string iTime = String.Format("{0:HH:mm:ss}", DateTime.Now);
            FileStream fs = new FileStream(iPathReportFile, FileMode.Append, FileAccess.Write);
            StreamWriter sw = new StreamWriter(fs);
            if (iStep == 0)
            {
                iTestCountGood = 0;
                iTestCountFail = 0;
                sw.WriteLine(@"<!DOCTYPE html>" + "\n" +
                    @"<html lang='ru-RU'>" + "\n" +
                    @"<head>" + "\n" +
                    @"<meta http-equiv='Content-Type' content='text/html; charset=UTF-8' />" + "\n" +
                    @"<title>Отчёт о тестировании</title>" + "\n" +
                    @"</head>" + "\n" +
                    @"<body>" + "\n" +
                    @"<div style='font-size:22px;' align='center'><strong>Тест начат: " + String.Format("{0:dd.MM.yyyy HH:mm:ss}", DateTime.Now) + @"</strong></div></br>" + "\n" +
                    @"<table border='1' align='center' cellpadding='5' cellspacing='0' width='100%'>" + "\n" +
                    @"<tr style='text-align:center;'>" + "\n" +
                    @"<td width='100px'><strong>Тест</strong></td>" + "\n" +
                    @"<td width='70px'><strong>Результат</strong></td>" + "\n" +
                    @"<td width='70px'><strong>Время</strong></td>" + "\n" +
                    @"<td width='70px'><strong>Снимок</strong></td>" + "\n" +
                    @"<td><strong>Сообщение</strong></td>" + "\n" +
                    @"</tr>");
            }
            if (iResult == true & iStep == 1)
            {
                iTestCountGood += 1;
                sw.WriteLine(@"<tr style='color: green;'>" + "\n" +
                    @"<td>" + iTestNum + @"</td>" + "\n" +
                    @"<td>Успех</td>" + "\n" +
                    @"<td>" + iTime + @"</td>" + "\n" +
                    @"<td>-</td>" + "\n" +
                    @"<td>" + iMessage + @"</td>" + "\n" +
                    @"</tr>" + "\n");
            }
            if (iResult == false & iStep == 1)
            {
                iTestCountFail += 1;
                ITakesScreenshot screenshootdrivers = Test.Browser as ITakesScreenshot;
                Screenshot screenshot = screenshootdrivers.GetScreenshot();
                //string iNameScreen = iTestNum + "_" + String.Format("{0:yyyy:-MM-dd_HH--mm-ss}", DateTime.Now);
                //screenshot.SaveAsFile("1213.png", OpenQA.Selenium.ScreenshotImageFormat.Png);
                //screenshot.SaveAsFile("d:\\page.png", OpenQA.Selenium.ScreenshotImageFormat.Png);
                screenshot.SaveAsFile(iFolderScreen + @"\" + iTestNum + ".png", OpenQA.Selenium.ScreenshotImageFormat.Png);
                sw.WriteLine(@"<tr style='color: red;'>" + "\n" +
                    @"<td>" + iTestNum + @"</td>" + "\n" +
                    @"<td>Провал</td>" + "\n" +
                    @"<td>" + iTime + @"</td>" + "\n" +
                    @"<td><center><a target='_blank' href='screen/" + iTestNum + ".png" + @"'>скриншот</a><br/><br/><a href='" + Browser.Url + @"' target='_blank'>URL</a></center></td>" + "\n" +
                    @"<td>" + iMessage + @"</td>" + "\n" +
                    @"</tr>" + "\n");
            }
            if (iStep == 9)
            {
                Decimal iProcent = 0;
                if (iTestCountGood > 0 || iTestCountFail > 0)
                {
                    iProcent = ((100 * iTestCountGood) / (iTestCountGood + iTestCountFail));
                }
                sw.WriteLine(@"<tr style='text-align:center;'>" + "\n" +
                    @"<td colspan='5'>&nbsp;</center></td>" + "\n" +
                    @"</tr>" + "\n" +
                    @"<tr style='text-align:center;'>" + "\n" +
                    @"<td colspan='5'>Всего тестов запущено: " + (iTestCountGood + iTestCountFail) + " || <span style='color: green;'>Успешно: " + iTestCountGood +
                             "</span> || <span style='color: red;'>Провалено: " + iTestCountFail + "</span> || Процент пройденных: " + iProcent + "% || Тест завершён: " + String.Format("{0:dd.MM.yyyy HH:mm:ss}", DateTime.Now) + "</td>" + "\n" +
                    @"</tr>" + "\n" +
                    @"</table>" + "\n" +
                    @"</body>" + "\n" +
                    @"</html>");
            }
            sw.Close();
        }
    }
}
