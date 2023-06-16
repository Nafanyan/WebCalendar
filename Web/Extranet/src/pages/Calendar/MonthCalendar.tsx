import { FunctionComponent, useEffect, useState } from 'react';
import { Button, Card, Table } from 'react-bootstrap';
import { IEvent } from '../../models/query/IEvent';
import { UserService } from '../../services/UserService';
import { IEventArray } from '../../models/IEventArray';
import { TimeToStringRequest, TimeToString } from '../../custom-functions/TimeToString';
import { fullDaysWeek, shortDaysWeek } from '../../constants/DayOfWeek';
import { AddEvent } from '../AddEvent';
import "../../css/month-calendar.css"


export interface MonthCalendarProps {
    userId: number
    month: number
    year: number
}

export const MonthCalendar: FunctionComponent<MonthCalendarProps> = ({ userId, month, year }) => {
    const [day2DArray, setDay2DArray] = useState<IEventArray[][]>([]);

    useEffect(() => {
        const create2DArray = async () => {
            let monthForCreateDate: number = month - 1;
            let monthDaysCount: number = new Date(year, month, 0).getDate();

            const service: UserService = new UserService();
            let startEventStr: string = TimeToStringRequest(new Date(year, monthForCreateDate, 1, 0, 0));
            let endEventStr: string = TimeToStringRequest(new Date(year, monthForCreateDate, monthDaysCount, 23, 59));
            let events: IEvent[] = await service.getEvent(userId, startEventStr, endEventStr);

            let daysOfMonth: IEventArray[] = [];
            for (let i = 1; i <= monthDaysCount; i++) {
                let emptyDate: Date = new Date(year, monthForCreateDate, i, 0, 0, 0);
                let emptyEvent: IEvent = {
                    id: userId,
                    name: "",
                    description: "",
                    startEvent: emptyDate,
                    endEvent: emptyDate
                }
                daysOfMonth.push({ arrayEvents: [emptyEvent] });
            }

            let currentEvent: IEvent;
            for (let i = 0; i < events.length; i++) {
                currentEvent = events[i];
                let nowDate: Date = new Date(currentEvent.startEvent.toString());
                let nowDayWeek = (nowDate.getDate() - 1);

                if (daysOfMonth[nowDayWeek].arrayEvents[0].name == "") {
                    daysOfMonth[nowDayWeek].arrayEvents.pop();
                }
                daysOfMonth[nowDayWeek].arrayEvents.push(currentEvent);
            }

            let dayWeekBeginMonth: number = new Date(year, monthForCreateDate, 1).getDay() - 1;
            if (dayWeekBeginMonth == -1) {
                dayWeekBeginMonth = 6;
            };
            let daysOfPrevMonth: IEventArray[] = [];
            for (let i = 1; i <= dayWeekBeginMonth; i++) {
                let emptyDate: Date = new Date(year, monthForCreateDate - 1, i, 0, 0, 0);
                let emptyEvent: IEvent = {
                    id: userId,
                    name: "",
                    description: "",
                    startEvent: emptyDate,
                    endEvent: emptyDate
                }
                daysOfPrevMonth.push({ arrayEvents: [emptyEvent] });
            }
            daysOfMonth = [...daysOfPrevMonth, ...daysOfMonth];

            let daysOfMonth2D: IEventArray[][] = [];
            for (let i = 0; i < monthDaysCount + dayWeekBeginMonth - 1; i += 7) {
                daysOfMonth2D.push(daysOfMonth.slice(i, i + 7));
            }
            setDay2DArray(daysOfMonth2D);
        };

        create2DArray();
    }, [userId, year, month])

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
                                        {new Date(day.arrayEvents[0].startEvent.toString()).getMonth() == month - 1 &&
                                            <Card className='day-of-months'>
                                                <Card.Header className='card-day-header'>
                                                    {new Date(day.arrayEvents[0].startEvent.toString()).getDate()}
                                                    <AddEvent userId={userId} day={day.arrayEvents[0].startEvent} />
                                                </Card.Header>

                                                <div className="scrollbar scrollbar-success">
                                                    {day.arrayEvents.map((eventsDay, keyEventDay) => (
                                                        <Card.Body id='card-day-text' key={keyEventDay} >
                                                            {eventsDay.name != "" &&
                                                                <Card.Text>
                                                                    <Button variant="light">
                                                                        {TimeToString(eventsDay.startEvent) + " - " + TimeToString(eventsDay.endEvent)}
                                                                    </Button>
                                                                    {" " + eventsDay.name}
                                                                </Card.Text>}

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