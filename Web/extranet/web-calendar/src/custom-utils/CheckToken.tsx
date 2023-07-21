import { AuthenticationService } from "../services/AuthenticationService";
import { TokenDecoder } from "./TokenDecoder";

export async function CheckToken() {
    if (localStorage.getItem('access-token') != null) {
        let tokenStr: string = "" + localStorage.getItem('access-token');
        console.log("go check")

        if (new Date() >= new Date(new Date(0).setSeconds(TokenDecoder(tokenStr).exp))) {
            console.log("go refhresh")

            const service = new AuthenticationService();
            await service.refreshToken();
        }
    }
}