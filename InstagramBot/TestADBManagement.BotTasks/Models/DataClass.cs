using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestADBManagement.BotTasks.Models
{
    public class DataClass
    {
        private string allData = "spyqhiyg:Q11DuwnO|w0lki@mail.ru;o0cek5 nouwlmevyj:xGl5vtZ2W|yacura_natasha@mail.ru;123456789aa gdrzkpiz:7pixatBx4|zi-moon@mail.ru;kukaliku7 lpjkwsfivd:FTTD1PlsjHx|dr_skossarev@mail.ru;98187 mcnkxioob:0Ob5iY9arrfy|vardbidzyan@mail.ru;vard&amp:&amp:&amp: jsqzrfqvu:w90dTRIySvw|valjatere@mail.ru;tibloko ydukgqucyb:MChJNvEpeEQmQ|v.l.a.d.i.mir.m1@mail.ru;19871952 gaicuxhc:Ob9ExgdAToU7|baevushka@mail.ru;Olg54955495 hupzyktrv:2TUKVIQNz|tamanka@inbox.ru;patentoved76 zbgzziyyde:UREMuHbAsf|t3schxg_3dak05@mail.ru;UtU8-2fjE fkesxdmg:Jus5Krvju7UdE|vinnikov2002@mail.ru;91546520a pzditrmx:9PSe7aPPu|zelenoglazii@list.ru;p9b2ns8a mdhywokb:2tFJfxGyt|bugtrackertest@mail.ru;gecnbntcerb iukxgkus:fsR2f1WG2TaD|sweets_1988@mail.ru;gjhfretvcz olqajqiwk:GEmhw6Q2mIpUu|ubcxow05jclqo5v@mail.ru;auofknityajt55 mfvopxfln:kBobWXpI0F8KL|symerki455@mail.ru;19971230bella aybtejcehk:UBBEGK0vda43T|zhavnerik64@bk.ru;19642104elena moywlmtek:U6aQMwYo2ik|zstmls5shbvgv83@mail.ru;oq2vvsqniik65g5 jjvlycaati:j0sRI8IvroNLX|tarah105bielik1992@mail.ru;ms6sh2D03";
        public List<InstagramAccount> Accounts { get; set; } = new List<InstagramAccount>();
        public void CreateAccountsList()
        {
            var allDataArray = allData.Split(' ');
            foreach (var item in allDataArray)
            {
                var accounts = item.Split('|');
                var instAcc = accounts[0].Split(':');
                var emailAcc = accounts[1].Split(';');
                var account = new InstagramAccount
                {
                    AccountName = instAcc[0],
                    InstagramPass = instAcc[1],
                    Email = emailAcc[0],
                    EmailPass = emailAcc[1]
                };
                Accounts.Add(account);
            }
        }
    }
}
