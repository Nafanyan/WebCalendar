import axios from "axios";
import config from "./../axios.conf.json";

export default axios.create({
    baseURL: config.baseURL,
    headers: {
        "Accept": "application/json",
        "Content-Type": "application/json",
    }
})