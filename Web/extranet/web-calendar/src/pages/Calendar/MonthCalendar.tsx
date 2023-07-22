import { FunctionComponent, useState, useEffect } from "react"
import { Table, Card } from "react-bootstrap"
import { shortDaysWeek } from "../../constants/DayOfWeek"
import { UserService } from "../../services/UserService"
import AddEvent from "./actions-with-events/addEvent"
import "../../css/calendar/month-calendar.css"
import { TimeToStringRequest } from "../../custom-utils/TimeToString"
import { useTypedSelector } from "../../hooks/UseTypeSelector"
import { IEvent } from "../../models/IEvent"
import { IDay } from "../../models/IEventArray"
import { EventInfo } from "./actions-with-events/eventInfo"

export const MonthCalendar: FunctionComponent = () => {
    const { userId, year, month, reRender } = useTypedSelector(state => state.currentDay);
    const [days, setDays] = useState<IDay[][]>([]);

    useEffect(() => {
        const dataInitialization = async () => {
            const service: UserService = new UserService();
            let startEventStr: string = TimeToStringRequest(new Date(year, month, 1, 0, 0));
            let endEventStr: string = TimeToStringRequest(new Date(year, month, monthDaysCount, 23, 59));
            console.log(userId)
            events = await service.getEvent(userId, startEventStr, endEventStr);

            fillState();
        };

        const fillState = () => {
            let daysOfMonth: IDay[] = [];
            for (let i = 1; i <= monthDaysCount; i++) {
                daysOfMonth.push({ arrayEvents: [], date: new Date(year, month, i, 0, 0, 0) });
            }

            let currentEvent: IEvent;
            for (let i = 0; i < events.length; i++) {
                currentEvent = events[i];
                let nowDate: Date = new Date(currentEvent.startEvent.toString());
                let nowDayWeek = (nowDate.getDate() - 1);
                daysOfMonth[nowDayWeek].arrayEvents.push(currentEvent);
            }

            let dayWeekBeginMonth: number = new Date(year, month, 1).getDay() - 1;
            if (dayWeekBeginMonth == -1) {
                dayWeekBeginMonth = 6;
            };

            let daysOfPrevMonth: IDay[] = [];
            for (let i = 1; i <= dayWeekBeginMonth; i++) {
                daysOfPrevMonth.push({ arrayEvents: [], date: new Date(year, month - 1, i, 0, 0, 0) });
            }
            daysOfMonth = [...daysOfPrevMonth, ...daysOfMonth];

            let daysOfMonth2D: IDay[][] = [];
            for (let i = 0; i < monthDaysCount + dayWeekBeginMonth; i += 7) {
                daysOfMonth2D.push(daysOfMonth.slice(i, i + 7));
            }
            setDays(daysOfMonth2D);
        };

        let events: IEvent[] = [];
        let monthDaysCount: number = new Date(year, month + 1, 0).getDate();

        dataInitialization();
    }, [year, month, reRender])

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
                                (nowDay, keyDay) => (
                                    <th key={keyDay}>
                                        {new Date(nowDay.date).getMonth() == month &&
                                            <Card className='day-of-months'>
                                                <Card.Header className='card-day-header'>
                                                    {new Date(nowDay.date.toString()).getDate()}
                                                    <AddEvent day={nowDay.date} />
                                                </Card.Header>

                                                <div className="scrollbar scrollbar-success">
                                                    {nowDay.arrayEvents.map((eventsDay, keyEventDay) => (
                                                        <Card.Body id='card-day-text' key={keyEventDay} >
                                                            {eventsDay != null &&
                                                                <Card.Text>
                                                                    <EventInfo startEvent={eventsDay.startEvent} endEvent={eventsDay.endEvent} />
                                                                    {" " + (eventsDay.name.length > 9 ? eventsDay.name.substring(0, 9) + "..." : eventsDay.name + " ")}
                                                                </Card.Text>
                                                            }
                                                        </Card.Body>
                                                    ))}
                                                </div>
                                            </Card>
                                        }
                                    </th>
                                )
                            )
                            }
                        </tr>
                    )
                )}
            </tbody>
        </Table>
    </div>)
}

export default MonthCalendar;