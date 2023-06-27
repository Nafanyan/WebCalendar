import { CurrentDayState, CurrentDayAction, CurrentDayActionType } from "../../models/type/currentDay"

const initialState: CurrentDayState = {
    userId: 4,
    year: new Date().getFullYear(),
    month: new Date().getMonth() + 1,
    day: new Date().getDate(),
    reRender: false
}

export const currentDayReducer = (state = initialState, action: CurrentDayAction): CurrentDayState => {
    switch (action.type) {
        case CurrentDayActionType.CHANGE_CURRENT_DAY:
            return {
                ...state, userId: action.userId,
                year: action.year,
                month: action.month,
                day: action.day
            }

        case CurrentDayActionType.FORCED_DEPENDENCY_RENDER:
            return {
                ...state, reRender: action.reRender
            }
        default:
            return state
    }
}