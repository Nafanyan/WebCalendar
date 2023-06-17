import { CurrentDay, CurrentDayAction, CurrentDayActionType } from "../../models/type/currentDay"

const initialState: CurrentDay = {
    userId: 4,
    year: new Date().getFullYear(),
    month: new Date().getMonth() + 1,
    day: new Date().getDate(),
    nextRendering: false
}

export const currentDayReducer = (state = initialState, action: CurrentDayAction): CurrentDay => {
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
                ...state, userId: state.userId,
                year: state.year,
                month: state.month,
                day: state.day,
                nextRendering: action.nextRendering
            }
        default:
            return state
    }
}