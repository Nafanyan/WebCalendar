import { FunctionComponent, useEffect, useState } from 'react';
import { Button, ButtonGroup, Card, Dropdown, DropdownButton, Nav, } from 'react-bootstrap';
import { SettingDateForUser } from '../models/SettingDateForUser';
import { useLocation } from 'react-router-dom';
import WeekCalendar from './Calendar/WeekCalendar';
import DayCalendar from './Calendar/DayCalendar';
import MonthCalendar from './Calendar/MonthCalendar';
import { months } from '../constants/Months';
import '../css/main-calendar.css'


export interface MainCalendarProps {
    mode: string,
    settingDate: SettingDateForUser
}

const SwitchModeContain: FunctionComponent<SettingDateForUser> = (settingDate) => {
    const location = useLocation()
    if (location.pathname === "/weeks") {
        return <WeekCalendar userId={settingDate.userId} day={settingDate.day} month={settingDate.month} year={settingDate.year} />;
    }
    else if (location.pathname === "/days") {
        return <DayCalendar userId={settingDate.userId} day={settingDate.day} month={settingDate.month} year={settingDate.year} />;
    }
    else {
        return <MonthCalendar userId={settingDate.userId} month={settingDate.month} year={settingDate.year} />;
    }
}

export const MainCalendar: FunctionComponent<MainCalendarProps> = ({ mode, settingDate }) => {
    const [nowYear, setNowYear] = useState<number>(settingDate.year);
    const [nowMonth, setNowMonth] = useState<number>(settingDate.month);
    const [nowDay, setNowDay] = useState<number>(settingDate.day);
    const [nowDate, setNowDate] = useState<Date>(new Date(nowYear, nowMonth - 1, nowDay));

    let nowDayWeek: number = nowDate.getDay() - 1;
    if (nowDayWeek == -1) {
        nowDayWeek = 6;
    }

    const [startWeekDay, setStartWeekDay] = useState<Date>(
        new Date(settingDate.year, settingDate.month - 1, settingDate.day - nowDayWeek));
    const [endWeekDay, setEndWeekDay] = useState<Date>(
        new Date(settingDate.year, settingDate.month - 1, settingDate.day - nowDayWeek + 6));

    useEffect(() => {
        setNowDay(nowDate.getDate());
        setNowMonth(nowDate.getMonth() + 1);
        setNowYear(nowDate.getFullYear());

        setStartWeekDay(
            new Date(nowDate.getFullYear(), nowDate.getMonth(), nowDate.getDate() - nowDayWeek));
        setEndWeekDay(
            new Date(nowDate.getFullYear(), nowDate.getMonth(), nowDate.getDate() - nowDayWeek + 6));
    }, [nowDate])

    return (
        <div>
            <Card className='nav-card-main'>
                <Card.Header className='nav-card-header'>
                    <Nav variant="tabs" defaultActiveKey={"/" + mode}>
                        <Nav.Item className='nav-link-months-year'>
                            <Nav.Link href="/months" >
                                Месяц
                            </Nav.Link>
                        </Nav.Item>

                        <Nav.Item className='nav-link-week'>
                            <Nav.Link href="/weeks" >
                                Неделя
                            </Nav.Link>
                        </Nav.Item>

                        <Nav.Item className='nav-link-day'>
                            <Nav.Link href="/days" >
                                День
                            </Nav.Link>
                        </Nav.Item>

                        <Nav.Item className='nav-group-button-date'>
                            {mode === "weeks" &&
                                <Nav.Item>
                                    <ButtonGroup >
                                        <Button id={'week-change-group'} onClick={() => {
                                            setNowDate(new Date(nowYear, nowMonth - 1, nowDay - 7));
                                        }}> {"◀"} </Button>

                                        <Button id={'week-change-group'}>{
                                            startWeekDay.getDate().toString() + "." + (startWeekDay.getMonth() + 1).toString() +
                                            " - " + endWeekDay.getDate().toString() + "." + (endWeekDay.getMonth() + 1).toString()
                                        }</Button>

                                        <Button id={'week-change-group'} onClick={() => {
                                            setNowDate(new Date(nowYear, nowMonth - 1, nowDay + 7));
                                        }}>{"▶"}</Button>

                                    </ButtonGroup>
                                </Nav.Item>
                            }

                            {mode === "days" &&
                                <Nav.Item>
                                    <ButtonGroup >
                                        <Button id={'day-change-group'} onClick={() => {
                                            setNowDate(new Date(nowYear, nowMonth - 1, nowDay - 1));
                                        }
                                        }>{"◀"}</Button>

                                        <Button id={'day-change-group'}>
                                            {nowDate.getDate().toString() + "." +
                                                (nowDate.getMonth() + 1).toString() + "." +
                                                nowDate.getFullYear().toString()}
                                        </Button>

                                        <Button id={'day-change-group'} onClick={() => {
                                            setNowDate(new Date(nowYear, nowMonth - 1, nowDay + 1));
                                        }}>{"▶"}</Button>
                                    </ButtonGroup>
                                </Nav.Item>
                            }

                            {mode === "months" &&
                                <Nav.Item>
                                    <ButtonGroup >
                                        <Button id={'year-change-group'} onClick={() => {
                                            nowYear > 2000 ?
                                                setNowDate(new Date(nowYear - 1, nowMonth - 1, nowDay)) : setNowYear(nowYear)
                                        }
                                        }>{"◀"}</Button>

                                        <Button id={'year-change-group'}>{nowYear}</Button>

                                        <Button id={'year-change-group'} onClick={() => {
                                            nowYear <= settingDate.year + 15 ?
                                                setNowDate(new Date(nowYear + 1, nowMonth - 1, nowDay)) : setNowYear(nowYear)
                                        }}>{"▶"}</Button>
                                    </ButtonGroup>

                                    <DropdownButton
                                        as={ButtonGroup}
                                        key={'month'}
                                        id={'dropdown-month-button'}
                                        title={months[nowMonth]}>
                                        {months.map(
                                            (month, keyMonth) => (
                                                <Dropdown.Item key={keyMonth} onClick={() => setNowDate(new Date(nowYear, keyMonth - 1, nowDay))} >
                                                    {month}
                                                </Dropdown.Item>
                                            )
                                        )}
                                    </DropdownButton>
                                </Nav.Item >
                            }
                        </Nav.Item>
                    </Nav>
                </Card.Header>

                <Card.Body className='content-card-body'>
                    <SwitchModeContain
                        userId={settingDate.userId}
                        year={nowYear}
                        month={nowMonth}
                        day={nowDay} />
                </Card.Body>
            </Card>
        </div>
    );
}

export default MainCalendar;