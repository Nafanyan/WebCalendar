export interface CurrentDay {
    userId: number;
    year: number;
    month: number;
    day: number;
    nextRendering?: boolean;
}

export enum CurrentDayActionType{
    CHANGE_CURRENT_DAY = "CHANGE_CURRENT_DAY",
    FORCED_DEPENDENCY_RENDER = "FORCED_RENDER"
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
    nextRendering: boolean;
}

export type CurrentDayAction =  ChangeCurrentDay | ForcedDependencyRender;