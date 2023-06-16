import { createContext } from "vm";

export interface IContextDateUser{
    userId: number;
    year: number;
    month: number;
    day: number;
}

const defaultState = {
    userId: 0,
    year: new Date().getFullYear(),
    month: new Date().getMonth() + 1,
    day: new Date().getDate(),
}

const DateContext = createContext<IContextDateUser>(defaultState);