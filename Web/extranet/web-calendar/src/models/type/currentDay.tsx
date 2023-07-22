export interface CurrentDayState {
    userId: number;
    year: number;
    month: number;
    day: number;
    reRender?: boolean;
}

export enum CurrentDayActionType{
    CHANGE_CURRENT_DAY = "CHANGE_CURRENT_DAY",
    FORCED_DEPENDENCY_RENDER = "FORCED_RENDER",
    CHANGE_USER_ID = "CHANGE_USER_ID"
}

interface ChangeCurrentDay{
    type: CurrentDayActionType.CHANGE_CURRENT_DAY;
    userId: number;
    year: number;
    month: number;
    day: number;
}

interface ForcedDependencyRender{
    type: CurrentDayActionType.FORCED_DEPENDENCY_RENDER;
    reRender: boolean;
}

interface ChangeUserId{
    type: CurrentDayActionType.CHANGE_USER_ID;
    userId: number;
}

export type CurrentDayAction =  ChangeCurrentDay | ForcedDependencyRender | ChangeUserId;