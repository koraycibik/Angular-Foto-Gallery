import { Component, OnInit } from '@angular/core';
import {FileUploader} from 'ng2-file-upload'
import {AlertifyService} from '../services/alertify.service'
import {AuthService} from '../services/auth.service'
import {ActivatedRoute} from '@angular/router'
import {Foto} from '../models/foto'

@Component({
  selector: 'app-foto',
  templateUrl: './foto.component.html',
  styleUrls: ['./foto.component.css']
})
export class FotoComponent implements OnInit {

  constructor(private authService:AuthService,
     private alertifyService:AlertifyService, 
     private activatedRoute:ActivatedRoute
  ) { }

  fotolar:Foto[]=[];
  up:FileUploader;
  hasBaseDropZoneOver =false;
  baseUrl='http://localhost:61061/api/';
  cMain:Foto;
  cYemek:any;

  ngOnInit() {
    this.activatedRoute.params.subscribe(params=>{
      this.cYemek = params["yemekId"]
    })
    this.initializeUploader();
  }



  initializeUploader(){
    this.up =new FileUploader({
      url:this.baseUrl +'yemekler/'+this.cYemek+'/foto',
      authToken: 'Bearer ' +localStorage.getItem('token'),
      isHTML5: true,
      allowedFileType : ['image'],
      autoUpload:false,
      removeAfterUpload: true,
      maxFileSize:10*1024*1024
    })

    this.up.onSuccessItem = (item, response, status, headers)=>{
      if(response){
        const cData :Foto = JSON.parse(response);
        const foto ={
          id:cData.id,
          url:cData.url,
          dateAdded:cData.EklemeZamani,
          description:cData.Aciklama,
          isMain:cData.VarMi,
          yemekId:cData.yemekId
        }
        this.fotolar.push(foto)
      }
    }
  }

}
