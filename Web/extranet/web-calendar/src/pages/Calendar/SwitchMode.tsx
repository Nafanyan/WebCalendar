import { FunctionComponent, useState, useEffect } from "react"
import { Nav, ButtonGroup, Button, DropdownButton, Dropdown } from "react-bootstrap"
import { useDispatch } from "react-redux"
import { useLocation } from "react-router-dom"
import { months } from "../../constants/Months"
import { useTypedSelector } from "../../hooks/useTypeSelector"
import { CurrentDayActionType } from "../../models/type/currentDay"
import "../../css/calendar/switch-mode.css"
import DayCalendar from "./DayCalendar"
import MonthCalendar from "./MonthCalendar"
import WeekCalendar from "./WeekCalendar"


export interface SwitchModeProps {
    mode: string
}

export const SwitchMode: FunctionComponent<SwitchModeProps> = ({ mode }) => {
    const currDay = useTypedSelector(state => state.currentDay)
    const dispatch = useDispatch()

    let nowDayWeek: number = new Date(currDay.year, currDay.month - 1, currDay.day).getDay() - 1
    if (nowDayWeek == -1) {
        nowDayWeek = 6
    }

    const [nowYear, setNowYear] = useState<number>(currDay.year)
    const [nowMonth, setNowMonth] = useState<number>(currDay.month)
    const [nowDay, setNowDay] = useState<number>(currDay.day)
    const [nowDate, setNowDate] = useState<Date>(new Date(nowYear, nowMonth - 1, nowDay))

    const [startWeekDay, setStartWeekDay] = useState<Date>(
        new Date(currDay.year, currDay.month - 1, currDay.day - nowDayWeek))
    const [endWeekDay, setEndWeekDay] = useState<Date>(
        new Date(currDay.year, currDay.month - 1, currDay.day - nowDayWeek + 6))

    useEffect(() => {
        setNowDay(nowDate.getDate())
        setNowMonth(nowDate.getMonth() + 1)
        setNowYear(nowDate.getFullYear())

        setStartWeekDay(
            new Date(nowDate.getFullYear(), nowDate.getMonth(), nowDate.getDate() - nowDayWeek))
        setEndWeekDay(
            new Date(nowDate.getFullYear(), nowDate.getMonth(), nowDate.getDate() - nowDayWeek + 6))

        dispatch({
            type: CurrentDayActionType.CHANGE_CURRENT_DAY,
            userId: currDay.userId,
            year: nowDate.getFullYear(),
            month: nowDate.getMonth() + 1,
            day: nowDate.getDate()
        })

    }, [nowDate])

    const previousDay = (event: React.MouseEvent<HTMLElement>) => {
        setNowDate(new Date(nowYear, nowMonth - 1, nowDay - 1))
    }

    const nextDay = (event: React.MouseEvent<HTMLElement>) => {
        setNowDate(new Date(nowYear, nowMonth - 1, nowDay + 1))
    }

    const previousWeek = (event: React.MouseEvent<HTMLElement>) => {
        setNowDate(new Date(nowYear, nowMonth - 1, nowDay - 7))
    }

    const nextWeek = (event: React.MouseEvent<HTMLElement>) => {
        setNowDate(new Date(nowYear, nowMonth - 1, nowDay + 7))
    }

    const previousYear = (event: React.MouseEvent<HTMLElement>) => {
        currDay.year > 2000 ? setNowDate(new Date(nowYear - 1, nowMonth - 1, nowDay)) : setNowYear(nowYear)
    }

    const nextYear = (event: React.MouseEvent<HTMLElement>) => {
        nowYear <= new Date().getFullYear() + 15 ? setNowDate(new Date(nowYear + 1, nowMonth - 1, nowDay)) : setNowYear(nowYear)
    }

    const changeMonth = (event: React.MouseEvent<HTMLElement>, numMonth: number) => {
        setNowDate(new Date(nowYear, numMonth - 1, nowDay))
    }

    return (
        <>
            {mode === "weeks" &&
                <Nav.Item>
                    <ButtonGroup >
                        <Button id={'week-change-group'} onClick={event => previousWeek(event)}> {"◀"} </Button>

                        <Button id={'week-change-group'}>{
                            startWeekDay.getDate().toString() + "." + (startWeekDay.getMonth() + 1).toString() +
                            " - " + endWeekDay.getDate().toString() + "." + (endWeekDay.getMonth() + 1).toString()
                        }</Button>

                        <Button id={'week-change-group'} onClick={event => nextWeek(event)}>{"▶"}</Button>

                    </ButtonGroup>
                </Nav.Item>
            }

            {mode === "days" &&
                <Nav.Item>
                    <ButtonGroup >
                        <Button id={'day-change-group'} onClick={event => previousDay(event)}>{"◀"}</Button>

                        <Button id={'day-change-group'}>
                            {nowDate.getDate().toString() + "." +
                                (nowDate.getMonth() + 1).toString() + "." +
                                nowDate.getFullYear().toString()}
                        </Button>

                        <Button id={'day-change-group'} onClick={event => nextDay(event)}>{"▶"}</Button>
                    </ButtonGroup>
                </Nav.Item>
            }

            {mode === "months" &&
                <Nav.Item>
                    <ButtonGroup >
                        <Button id={'year-change-group'} onClick={event => previousYear(event)}>{"◀"}</Button>

                        <Button id={'year-change-group'}>{nowYear}</Button>

                        <Button id={'year-change-group'} onClick={event => nextYear(event)}>{"▶"}</Button>
                    </ButtonGroup>

                    <DropdownButton
                        as={ButtonGroup}
                        key={'month'}
                        id={'dropdown-month-button'}
                        title={months[nowMonth]}>
                        {months.map(
                            (month, keyMonth) => (
                                <Dropdown.Item key={keyMonth} onClick={event => changeMonth(event, keyMonth)} >
                                    {month}
                                </Dropdown.Item>
                            )
                        )}
                    </DropdownButton>
                </Nav.Item >
            }
        </>)
}

export const SwitchModeContain: FunctionComponent = () => {
    const location = useLocation();

    if (location.pathname === "/weeks") {
        return <WeekCalendar/>
    }
    else if (location.pathname === "/days") {
        return <DayCalendar />
    }
    else {
        return <MonthCalendar />
    }
}