using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication1
{
    class Program
    {
        static void Main(string[] args)
        {
            int selection = 5;
            List<Schedule> schedule = new List<Schedule>();
            Pay payment;
            while (selection!=4)
            {
                Console.WriteLine("-----Selection-------");
                Console.WriteLine("Enter the selection: Schedule/Pay/Tax");
                string Selection;
                Selection = Console.ReadLine();
                Selection = Selection.ToLower();
                if (Selection.ToLower() == "schedule")
                {
                    selection = 1;
                    
                    schedule.Add(new Schedule("Monday",false));
                    schedule.Add(new Schedule("Tuesday",false));
                    schedule.Add(new Schedule("Wednesday",false));
                    schedule.Add(new Schedule("Thursday",false));
                    schedule.Add(new Schedule("Friday",false));
                    foreach( Schedule s in schedule)
                    {
                        s.take_input();
                    }
                    Console.WriteLine("Enter the day");
                    string day;
                    day = Console.ReadLine();
                    day = day.ToLower();
                    foreach(Schedule s in schedule)
                    {
                        if (s.day.ToLower() == day.ToLower())
                        {
                            s.print_schedule();
                        }
                    }

                }
                else if (Selection.ToLower() == "pay")
                {
                    selection = 2;
                    if (schedule == null)
                    {
                        Console.WriteLine("Schedule not entered for this week!\n Unable to calculate the payment");
                        selection = 5;
                        continue;
                    }
                    payment = new Pay(schedule);
                    Console.Write("This week worked hours are : ");
                    payment.worked_hours();
                    Console.WriteLine(payment.hours_worked);
                    payment.print_payment();
                }else if (Selection == "tax")
                {
                    selection = 3;
                }
                selection = 5;
            }
        }

    }
    class Schedule
    {
        public int work_hours = 0;
        public string day;
        public bool is_holiday;
        public List<string> start_time; 
        public List<string> end_time;
        public int overtime = 0;
        public Schedule(string day,bool holiday)
        {
            this.day = day;
            this.work_hours = 0;
            this.start_time = new List<string>();
            this.end_time = new List<string>();
            this.is_holiday = holiday;
            this.overtime = 0;
        }
        public void take_input()
        {
            Console.WriteLine("Enter " + this.day + " Schedule:");
            Console.WriteLine("Enter From :");
            string from = Console.ReadLine();
            Console.WriteLine("Enter to:");
            string to = Console.ReadLine();
            this.start_time.Add(from);
            this.end_time.Add(to);
        }
        int convert_to_int(string s)
        {
            StringBuilder num = new StringBuilder();
            int pos = 0;
            bool flag = false;
            foreach (var c in s)
            {
                if (c >= '0' && c <= '9')
                {
                    num.Insert(pos, c);
                    pos++;
                }else if (c == 'a' || c == 'p')
                {
                    if (c == 'p')
                    {
                        flag = true;
                    }
                }
            }

            string time = num.ToString();
            int itime = 0;
            foreach(var c in time)
            {
                itime = itime * 10 + (c - '0');
            }
            if (flag == true)
            {
                itime += 12;
            }
            return itime;
        }
        public void evaluate_today_time(){
            for(int i = 0; i < start_time.Count; ++i)
            {
                int from = convert_to_int(start_time[i]);
                int to = convert_to_int(end_time[i]);
                if (to > 16)
                {
                    this.overtime += to - 16;
                    to = 16;
                }
                this.work_hours += (to - from); 
            }
        }
        public void print_schedule()
        {
            Console.WriteLine(this.day + "\'s Schedule is:");
            /*foreach(string s in start_time)
            {
                Console.WriteLine(s);
            }*/
            for(int i = 0; i < start_time.Count; ++i)
            {
                Console.WriteLine(start_time[i] + " to " + end_time[i]);
            }
        }
    }
    class Pay
    {
        public int hours_worked;
        public int over_time;
        public int holidays;
        public int total_payment;
        public List<Schedule> schedule;
        public Pay(List<Schedule> schedule)
        {
            this.hours_worked = 0;
            this.over_time = 0;
            this.holidays = 0;
            this.total_payment = 0;
            this.schedule = schedule;
        }
        public void worked_hours()
        {
            foreach(Schedule s in this.schedule)
            {
                hours_worked += s.work_hours;
                over_time += s.overtime;
            }
        }
        //considering overtime chargers is 1.5 times
        public void print_payment()
        {
            int amount = this.hours_worked * 20 + this.over_time * 30;
            Console.WriteLine("This week payment is :" + amount + " $");
        }
    }
    class Tax{
        bool exempt;
        int total_amount;
        float tax_rate;
        Tax()
        {
            this.exempt = false;
            this.total_amount = 0;
        }
        public void is_exempt()
        {
            Console.WriteLine("Is Exempt or non exempt");
            string ans;
            ans = Console.ReadLine();
            ans = ans.ToLower();
            if (ans == "exempt")
            {
                exempt = true;
            }else
            {
                exempt = false;
            }
        }
        //Not complete here......
        public void choose_tax_rate()
        {
            Console.WriteLine("Choose tax rate: ");

        }
    }
}
