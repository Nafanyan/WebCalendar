import { FunctionComponent, useEffect } from "react";
import { useTypedSelector } from "../hooks/useTypeSelector";
import { useDispatch } from "react-redux";
import { CurrentDayActionType } from "../models/type/currentDay";

const UserList: FunctionComponent = () => {
    const currDay = useTypedSelector(state => state.currentDay);
    const {year, month, day} = useTypedSelector(state => state.currentDay);
    const dispatch = useDispatch()

    useEffect(() => {
        console.log("change date")
    }, [currDay])

    const nextDay = (event: React.MouseEvent<HTMLElement>, day: number) => {
        dispatch({
            type: CurrentDayActionType.CHANGE_CURRENT_DAY,
            userId: currDay.userId,
            year: currDay.year,
            month: currDay.month,
            day: currDay.day + day
        })
    }

    return (<>
        <button onClick={event => nextDay(event, 1)} >Next Day</button>

        {/* <button onClick={nextDay}></button> */}
    </>);
}


export default UserList;