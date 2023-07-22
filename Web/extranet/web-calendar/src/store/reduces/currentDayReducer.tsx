import { TokenDecoder } from "../../custom-utils/TokenDecoder"
import { CurrentDayState, CurrentDayAction, CurrentDayActionType } from "../../models/type/currentDay"

const initialState: CurrentDayState = {
    userId: localStorage.getItem('access-token') != null ? TokenDecoder(localStorage.getItem('access-token') + "").userId : 0,
    year: new Date().getFullYear(),
    month: new Date().getMonth(),
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
        case CurrentDayActionType.CHANGE_USER_ID:
            return {
                ...state, userId: action.userId
            }
        default:
            return state
    }
}