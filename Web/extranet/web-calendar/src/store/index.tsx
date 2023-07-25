import { rootReducer } from "./reduces";
import { createStore } from "redux"

export const store = createStore(rootReducer);
