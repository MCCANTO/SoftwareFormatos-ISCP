

export interface StatusResponse {
    ok: boolean;
    message: string;
    messages: string[];
    data: any;
}


export interface Response<T> {
    ok: boolean;
    message: string;
    messages: string[];
    data: T;
}

export interface Response<T> {
  success: boolean;
  statusCode: number;
  messages: string[];
  data: T;
}
