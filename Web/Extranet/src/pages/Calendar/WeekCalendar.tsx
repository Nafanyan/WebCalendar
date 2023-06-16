import React, { FunctionComponent, useEffect, useState } from 'react';
import { Button, Card, Table } from 'react-bootstrap';
import { IEvent } from '../../models/query/IEvent';
import { UserService } from '../../services/UserService';
import { IEventArray } from '../../models/IEventArray';
import { TimeToStringRequest, TimeToString } from '../../custom-functions/TimeToString';
import { shortDaysWeek } from '../../constants/DayOfWeek';
import AddEvent from '../AddEvent';
import "../../css/week-calendar.css"

export interface WeekCalendarProps {
    userId: number
    day: number
    month: number
    year: number
}

export const WeekCalendar: FunctionComponent<WeekCalendarProps> = ({ userId, day, month, year }) => {
    const [day2DArray, setDay2DArray] = useState<IEventArray[][]>([]);

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

        create2DArray();
    }, [userId, day, month, year])

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
                                                <AddEvent userId={userId} day={day.arrayEvents[0].startEvent} />
                                            </Card.Header>

                                            <div className="scrollbar scrollbar-success">
                                                {day.arrayEvents.map((eventsDay, keyEventDay) => (

                                                    <Card.Body className='card-day-text' key={keyEventDay}>
                                                        {eventsDay.name != "" &&
                                                            <Card.Text >
                                                                <Button variant="light">
                                                                    {TimeToString(eventsDay.startEvent) + " - " + TimeToString(eventsDay.endEvent)}
                                                                </Button>
                                                                {" " + eventsDay.name}
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
