import axios from "axios";

export default axios.create({
    // for debug
    baseURL: "http://localhost:24395/",
    headers: {
        "Accept": "application/json",
        "Content-Type": "application/json",
    }
})