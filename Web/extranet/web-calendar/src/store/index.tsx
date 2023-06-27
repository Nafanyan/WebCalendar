import thunk from "redux-thunk";
import { rootReducer } from "./reduces";
import { createStore, applyMiddleware } from 'redux'
import { CurrentDayState } from "../models/type/currentDay";

// convert object to string and store in localStorage
function saveToLocalStorage(state: CurrentDayState) {
    try {
        const serialisedState = JSON.stringify(state);
        localStorage.setItem("persistantState", serialisedState);
    } catch (e) {
        console.warn(e);
    }
}

// load string from localStarage and convert into an Object
// invalid output must be undefined
function loadFromLocalStorage() {
    try {
        const serialisedState = localStorage.getItem("persistantState");
        if (serialisedState === null) return undefined;
        return JSON.parse(serialisedState);
    } catch (e) {
        console.warn(e);
        return undefined;
    }
}

// create our store from our rootReducers and use loadFromLocalStorage
// to overwrite any values that we already have saved
export const store = createStore(rootReducer, loadFromLocalStorage(), applyMiddleware(thunk));

// listen for store changes and use saveToLocalStorage to
// save them to localStorage
store.subscribe(() => {
    saveToLocalStorage(store.getState().currentDay)
})

export default store;