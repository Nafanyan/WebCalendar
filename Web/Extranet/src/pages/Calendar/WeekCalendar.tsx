import React, { FunctionComponent, useEffect, useState } from 'react';
import { Button, Card, Table } from 'react-bootstrap';
import "../../css/week-calendar.css"
import { IEvent } from '../../models/IEvent';
import { UserService } from '../../services/UserService';
import { IEventArray } from '../../models/IEventArray';
import { TimeToString, TimeToStringRequest } from '../../CustomFunctions/TimeToString';

export interface WeekCalendarProps {
    userId: number
    day: number
    month: number
    year: number
}

export const WeekCalendar: FunctionComponent<WeekCalendarProps> = ({ userId, day, month, year }) => {
    const [day2DArray, setDay2DArray] = useState<IEventArray[][]>([]);
    let daysWeek: string[] = ['ПН', 'ВТ', 'СР', 'ЧТ', 'ПТ', 'СБ', 'ВС'];

    useEffect(() => {

        const fetchEvents = async () => {
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
                    id: userId,
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

        fetchEvents();
    }, [])

    return (<div>
        <Table striped bordered hover>
            <thead>
                <tr>
                    {daysWeek.map((day, keyWeekDay) =>
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
                                        {day.arrayEvents.map((eventsDay, keyEventDay) => (
                                            <Card className='day-of-weeks' key={keyEventDay}>
                                                <Card.Header className='card-day-header'>
                                                    {new Date(eventsDay.startEvent.toString()).getDate()}
                                                    <button className='add-event-button'>+</button>
                                                </Card.Header>
                                                <Card.Body className='card-day-text'>
                                                    {eventsDay.name != "" &&
                                                        <Card.Text >
                                                            <Button variant="light">
                                                                {TimeToString(eventsDay.startEvent) + " - " + TimeToString(eventsDay.endEvent)}
                                                            </Button>
                                                            {" " + eventsDay.name}
                                                        </Card.Text>}
                                                </Card.Body>
                                            </Card>
                                        ))}
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