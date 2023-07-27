import { ITokenCommandResult } from "../models/result-request/Tokens/ITokenCommandResult";
import { IAuthentication } from "../models/command/Authentication/IAuthentication";
import AxiosBases from "./AxiosBases";
import { Cookies } from "react-cookie";

export class AuthenticationService {

    async refreshToken(): Promise<ITokenCommandResult> {
        const cookies = new Cookies();
        AxiosBases.defaults.withCredentials = true;
        
        let response: ITokenCommandResult = await AxiosBases.post("Api/Users/Refresh-Token")
            .then((response) => {
                localStorage.removeItem("access-token")
                localStorage.setItem("access-token", JSON.stringify(response.data.objResult.accessToken))
                localStorage.setItem("token-is-valid", JSON.stringify(true))
                cookies.set("RefreshToken", response.data.objResult.refreshToken)
                return response.data;
            }).catch(error => {
                localStorage.removeItem("token-is-valid")
                cookies.remove("RefreshToken")
                return error.response.data
            })
        return response;
    }

    async authentication(body: IAuthentication): Promise<ITokenCommandResult> {
        localStorage.removeItem("user-name")
        localStorage.setItem("user-name", JSON.stringify(body.Login));

        const cookies = new Cookies();
        let response: ITokenCommandResult = await AxiosBases.post("Api/Users/Authentication", body).then((response) => {
            localStorage.removeItem("access-token")
            localStorage.setItem("access-token", JSON.stringify(response.data.objResult.accessToken))
            localStorage.setItem("token-is-valid", JSON.stringify(true))
            cookies.set("RefreshToken", response.data.objResult.refreshToken)
            return response.data;
        }).catch(error => {
            return error.response.data
        })
        return response;
    }
}