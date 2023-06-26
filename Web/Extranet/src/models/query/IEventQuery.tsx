import { IEvent } from "../IEvent";
import { IValidationResult } from "../IValidationResult";

export interface IEventQueryResult{
    validationResult: IValidationResult,
    objResult: IEvent
}