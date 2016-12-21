using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using WebApplication1.Models;

namespace WebApplication1.DAL
{
    public class NurseEventsInitializer : System.Data.Entity.DropCreateDatabaseIfModelChanges<NurseEventContext>
    {
        protected override void Seed(NurseEventContext context)
        {
            var nurseevents = new List<NurseEvent>
            {
                new NurseEvent{ 
                                ID=9849,
                                patient_name="c罗",
                                dept_name ="01",
                                bed_no = "06",
                                patient_age="51",
                                patient_sex ="男",
                                charge_type = "计划弓弩一股",
                                diagnosis = "银行业还有个别",
                                inp_no="65418",
                                admission_date=DateTime.Parse("2002-09-01 12:00:00"),
                                event_happen_time=DateTime.Parse("2002-09-01"),
                                event_info ="地方报表男",
                                event_level="空间沙大部分",
                                event_service="交汇点嫂夫人",
                                event_name ="教员们",
                                event_fact="舌乳头",
                                event_relevant_factor="天赋一套语言",
                                event_patient_status="的怀仁堂交换",
                                event_harm="阿污染物当然",
                                event_harm_grade="打算通过",
                                event_location="核反应估计他",
                                event_concerned_staff1="而我国特人与人",
                                event_concerned_staff2="给东南亚虎头",
                                event_concerned_staff3 ="规范村民方面",
                                event_treatment = "科技大开发机构",
                                event_treatment_after = "会同离开捷克",
                                reporter_name = "犹太人一特任",
                                reporter_id = "俄武器日期",
                                handled_by_headnurse = "即空格符",
                                handled_by_leader = "看定界符广播剧客人"
                               }
            };
            nurseevents.ForEach(s => context.NurseEvents.Add(s));
            context.SaveChanges();
        }
    }
}