import { combineReducers } from "redux";
import { currentDayReducer } from "./currentDayReducer";


export const rootReducer = combineReducers({
    currentDay: currentDayReducer
}) 

export type RootState = ReturnType<typeof rootReducer>