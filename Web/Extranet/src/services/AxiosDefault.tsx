import axios from "axios";

export default axios.create({
<<<<<<< HEAD
    baseURL: "http://localhost:5157/",
=======
    baseURL: "http://localhost:5181/",
>>>>>>> 125585c430fe776b09da1b081f306bfe240cf94f
    headers: {
        "Accept": "application/json",
        "Content-Type": "application/json"
    }
});