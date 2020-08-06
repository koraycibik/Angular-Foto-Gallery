import { Injectable } from "@angular/core";
import { HttpClient } from "@angular/common/http";
import { Observable } from "rxjs";
import { Yemek } from "../models/yemek";
import { Foto } from "../models/foto";
import { AlertifyService } from "./alertify.service";
import { Router } from "@angular/router";

@Injectable({
  providedIn: "root"
})
export class YemekService {
  constructor(private httpClient: HttpClient,    
    private alertifyService:AlertifyService,
    private router:Router) {}
  path = "http://localhost:61061/api/";

  getYemekler(): Observable<Yemek[]> {
    return this.httpClient.get<Yemek[]>(this.path + "yemekler");
  }
  getYemekId(yemekId):Observable<Yemek>{
    return this.httpClient.get<Yemek>(this.path+"yemekler/detail/?id="+yemekid)
  }

  getFotoYemek(yemekId):Observable<Foto[]>{
    return this.httpClient.get<Foto[]>(this.path + "yemekler/fotolar/?yemekid="+yemekid);
  }

 add(yemek){
  this.httpClient.post(this.path + 'yemekler/add',yemek).subscribe(d=>{
    this.alertifyService.success("Yemek eklendi.")
    this.router.navigateByUrl('/yemekDetay/'+d["id"])
  });
 }
}
