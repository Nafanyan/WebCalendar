import { IValidationResult } from "../../IValidationResult";

export interface IUserQueryResult {
    validationResult: IValidationResult,
    objResult: {
        id: number,
        login: string
    }
}

