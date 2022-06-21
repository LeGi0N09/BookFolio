import FilterModel from "../models/FilterModel";

// http status codes
export const StatusCode = {
    Success: 200,
    BadRequest: 400,
    Unauthorized: 401,
    Forbidden: 403,
    Conflict: 409,
    InternalServer: 500
};

export const colors = {
    primary: "#f14d54",
    white: "#FFFFFF",
    textColor: "#414141",
    grayBg: "#f4f4f4",
    grayText: "#666666",
    grayBorder: "#cacaca",
    lightGrayBorder: "#e5e5e5",
    lightTextColor: "#838383",
    greenText: "#80BF32",
};

export const defaultFilter: FilterModel = {
    pageIndex: 1,
    pageSize: 10,
    keyword: ""
}

export const RecordsPerPage: number[] = [2, 5, 10, 100]