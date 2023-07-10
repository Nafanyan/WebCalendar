import { IEvent } from "../IEvent";
import { IValidationResult } from "../IValidationResult";

export interface IEventsQueryResult{
    validationResult: IValidationResult,
    objResult: IEvent[]
}