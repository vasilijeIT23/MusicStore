import { Injectable } from "@angular/core";
import { BehaviorSubject } from "rxjs";

@Injectable({
    providedIn: "root"
})
export class AuthService {
    token$ = new BehaviorSubject<null | string>(null);
    role$ = new BehaviorSubject<null | string>(null);
    //id$ = new BehaviorSubject<null | string>(null);
}