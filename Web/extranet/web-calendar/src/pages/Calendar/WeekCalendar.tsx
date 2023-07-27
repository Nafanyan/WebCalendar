import { FunctionComponent, useState, useEffect } from "react";
import { Table, Card, Button } from "react-bootstrap";
import { shortDaysWeek } from "../../constants/DayOfWeek";
import { UserService } from "../../services/UserService";
import "../../css/calendar/week-calendar.css";
import AddEvent from "./actions-with-events/addEvent";
import EventInfo from "./actions-with-events/eventInfo";
import { TimeToStringRequest } from "../../custom-utils/TimeToString";
import { useTypedSelector } from "../../hooks/UseTypeSelector";
import { IEvent } from "../../models/IEvent";
import { IDay } from "../../models/IDay";
import { ChartForWeek } from "./ChartForWeek";


export const WeekCalendar: FunctionComponent = () => {
    const [days, setDays] = useState<IDay[][]>([]);
    const { userId, year, month, day, reRender } = useTypedSelector(state => state.currentDay);

    useEffect(() => {
        const dataInitialization = async () => {
            const service: UserService = new UserService();
            let startEventStr: string = TimeToStringRequest(new Date(year, month, day - dayWeekBeginMonth, 0, 0));
            let endEventStr: string = TimeToStringRequest(new Date(year, month, day + 6 - dayWeekBeginMonth, 23, 59));
            events = await service.getEvent(userId, startEventStr, endEventStr);

            fillState();
        };

        const fillState = () => {
            let daysOfMonth: IDay[] = [];
            for (let i = day - dayWeekBeginMonth; i <= day + 7 - dayWeekBeginMonth; i++) {
                daysOfMonth.push({ arrayEvents: [], date: new Date(year, month, i, 0, 0, 0) });
            }

            let currentEvent: IEvent;
            for (let i = 0; i < events.length; i++) {
                currentEvent = events[i];
                let nowDate: Date = new Date(currentEvent.startEvent.toString());
                let nowDayWeek = (nowDate.getDay() - 1);

                if (nowDayWeek == -1) {
                    nowDayWeek = 6;
                }
                daysOfMonth[nowDayWeek % 7].arrayEvents.push(currentEvent);
            }

            let daysOfMonth2D: IDay[][] = [];
            daysOfMonth2D.push(daysOfMonth.slice(0, 7));
            setDays(daysOfMonth2D);
        }

        let events: IEvent[] = [];
        let dayWeekBeginMonth: number = new Date(year, month, day).getDay() - 1;
        if (dayWeekBeginMonth == -1) {
            dayWeekBeginMonth = 6;
        }

        dataInitialization();
    }, [userId, day, month, year, reRender]);

    return (<div>
        <Table striped bordered hover>
            <thead>
                <tr>
                    {shortDaysWeek.map((day, keyWeekDay) =>
                    (
                        <th className='day-of-week' key={keyWeekDay}>
                            {day}
                        </th>
                    )
                    )}
                </tr>
            </thead>

            <tbody>
                {days.map(
                    (week, keyWeek) => (
                        <tr key={keyWeek}>
                            {week.map(
                                (day, keyDay) => (
                                    <th key={keyDay}>
                                        <Card className='day-of-weeks' >
                                            <Card.Header className='card-day-header'>
                                                {new Date(day.date.toString()).getDate()}
                                                <AddEvent day={day.date} />
                                            </Card.Header>

                                            <div className="scrollbar scrollbar-success">
                                                {day.arrayEvents.map((eventsDay, keyEventDay) => (

                                                    <Card.Body className='card-day-text' key={keyEventDay}>
                                                        {eventsDay != null &&
                                                            <Card.Text >
                                                               <EventInfo startEvent={eventsDay.startEvent} endEvent={eventsDay.endEvent} />
                                                               {" " + (eventsDay.name.length > 12 ? eventsDay.name.substring(0,12) + "..." : eventsDay.name + " ")}
                                                            </Card.Text>}
                                                    </Card.Body>
                                                ))}
                                            </div>
                                        </Card>
                                    </th>
                                )
                            )
                            }
                        </tr>
                    )
                )}
            </tbody>
        </Table>
        <ChartForWeek weeks={days}/>
    </div>)
}

export default WeekCalendar;