
import { FunctionComponent, useState, useEffect } from "react";
import { Table, Card } from "react-bootstrap";
import { shortDaysWeek } from "../../constants/DayOfWeek";
import "../../css/calendar/week-calendar.css"
import { TimeToStringRequest } from "../../custom-functions/TimeToString";
import { useTypedSelector } from "../../hooks/useTypeSelector";
import { IEvent } from "../../models/IEvent";
import { IEventArray } from "../../models/IEventArray";
import { UserService } from "../../services/UserService";
import AddEvent from "./actions-with-events/AddEvent";
import EventInfo from "./actions-with-events/EventInfo";



export const WeekCalendar: FunctionComponent = () => {
    const [day2DArray, setDay2DArray] = useState<IEventArray[][]>([]);
    const { userId, year, month, day, nextRendering } = useTypedSelector(state => state.currentDay);

    useEffect(() => {
        const create2DArray = async () => {
            let monthForCreateDate: number = month - 1;
            let dayWeekBeginMonth: number = new Date(year, monthForCreateDate, day).getDay() - 1;
            if (dayWeekBeginMonth == -1) {
                dayWeekBeginMonth = 6;
            }

            const service: UserService = new UserService();
            let startEventStr: string = TimeToStringRequest(new Date(year, monthForCreateDate, day - dayWeekBeginMonth, 0, 0));
            let endEventStr: string = TimeToStringRequest(new Date(year, monthForCreateDate, day + 6 - dayWeekBeginMonth, 23, 59));
            let events: IEvent[] = await service.getEvent(userId, startEventStr, endEventStr);

            let daysOfMonth: IEventArray[] = [];
            for (let i = day - dayWeekBeginMonth; i <= day + 7 - dayWeekBeginMonth; i++) {
                let emptyEvent: IEvent = {
                    userId: userId,
                    name: "",
                    description: "",
                    startEvent: new Date(year, monthForCreateDate, i, 0, 0, 0),
                    endEvent: new Date(year, monthForCreateDate, i, 0, 0, 0)
                }
                daysOfMonth.push({ arrayEvents: [emptyEvent] });
            }

            let currentEvent: IEvent;
            for (let i = 0; i < events.length; i++) {
                currentEvent = events[i];
                let nowDate: Date = new Date(currentEvent.startEvent.toString());
                let nowDayWeek = (nowDate.getDay() - 1);
                if (nowDayWeek == -1) {
                    nowDayWeek = 6;
                }
                if (daysOfMonth[nowDayWeek % 7].arrayEvents[0].name == "") {
                    daysOfMonth[nowDayWeek % 7].arrayEvents.pop();
                }
                daysOfMonth[nowDayWeek % 7].arrayEvents.push(currentEvent);
            }

            let daysOfMonth2D: IEventArray[][] = [];
            daysOfMonth2D.push(daysOfMonth.slice(0, 7));
            setDay2DArray(daysOfMonth2D);
        }

        create2DArray();
    }, [userId, day, month, year, nextRendering])

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
                {day2DArray.map(
                    (week, keyWeek) => (
                        <tr key={keyWeek}>
                            {week.map(
                                (day, keyDay) => (
                                    <th key={keyDay}>
                                        <Card className='day-of-weeks' >
                                            <Card.Header className='card-day-header'>
                                                {new Date(day.arrayEvents[0].startEvent.toString()).getDate()}
                                                <AddEvent day={day.arrayEvents[0].startEvent} />
                                            </Card.Header>

                                            <div className="scrollbar scrollbar-success">
                                                {day.arrayEvents.map((eventsDay, keyEventDay) => (

                                                    <Card.Body className='card-day-text' key={keyEventDay}>
                                                        {eventsDay.name != "" &&
                                                            <Card.Text >
                                                               <EventInfo startEvent={eventsDay.startEvent} endEvent={eventsDay.endEvent} />
                                                               {" " + (eventsDay.name.length > 9 ? eventsDay.name.substring(0,9) + "..." : eventsDay.name + " ")}
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
    </div>)
}

export default WeekCalendar;