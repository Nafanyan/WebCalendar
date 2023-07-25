import { AuthenticationService } from "../services/AuthenticationService";
import { TokenDecoder } from "./TokenDecoder";

export async function CheckToken() {
    if (localStorage.getItem('access-token') != null) {
        let tokenStr: string = "" + localStorage.getItem('access-token');
        if (new Date() >= new Date(new Date(0).setSeconds(TokenDecoder(tokenStr).exp))) {
            let service: AuthenticationService = new AuthenticationService();
            await service.refreshToken();
        }
    }
}