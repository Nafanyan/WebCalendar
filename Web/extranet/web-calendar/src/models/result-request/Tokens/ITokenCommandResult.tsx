import { IValidationResult } from "../../IValidationResult";

export interface ITokenCommandResult {
    validationResult: IValidationResult,
    objResult: {
        accessToken: string,
        refreshToken: string
    }
}