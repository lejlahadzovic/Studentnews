import { Injectable } from '@angular/core';
import { AngularFireDatabase, AngularFireList } from '@angular/fire/compat/database';
import { AngularFireStorage } from '@angular/fire/compat/storage';
import {AutentifikacijaHelper} from "../helper/autentifikacija-helper";
import { Observable } from 'rxjs';
import { finalize } from 'rxjs/operators';
import { FileUpload } from '../models/file-upload.model';
import { map } from 'rxjs/operators';
@Injectable({
  providedIn: 'root'
})
export class FileUploadService {
  private basePath = '/korisnickeSlike';

  constructor(private db: AngularFireDatabase, private storage: AngularFireStorage) { }

  pushFileToStorage(fileUpload: FileUpload): Observable<number | undefined> {
    const filePath = `${this.basePath}/${fileUpload.file.name}`;
    const storageRef = this.storage.ref(filePath);
    const uploadTask = this.storage.upload(filePath, fileUpload.file);

    uploadTask.snapshotChanges().pipe(
      finalize(() => {
        storageRef.getDownloadURL().subscribe(downloadURL => {
          fileUpload.url = downloadURL;
          fileUpload.name = fileUpload.file.name;
          this.saveFileData(fileUpload);
        });
      })
    ).subscribe();

    return uploadTask.percentageChanges();
  }

  private saveFileData(fileUpload: FileUpload): void {
    const korisnikId = AutentifikacijaHelper.getLoginInfo().autentifikacijaToken?.korisnik.id;
    if (korisnikId) {
      this.getUserImageEntry(korisnikId).subscribe(entry => {
        if (entry) {
          const existingEntry = {...entry.payload.val()};
          existingEntry.url = fileUpload.url;

          this.db.object(`${this.basePath}/${entry.key}`).update(existingEntry);
        } else {
          const fileData = {
            korisnikId: korisnikId,
            url: fileUpload.url
          };
          this.db.list(this.basePath).push(fileData);
        }
      });
    }
  }

  private getUserImageEntry(korisnikId: number): Observable<any> {
    return this.db.list(this.basePath, ref =>
      ref.orderByChild('korisnikId').equalTo(korisnikId)
    ).snapshotChanges().pipe(
      map(actions => {
        if (actions.length > 0) {
          return actions[0];
        } else {
          return null;
        }
      })
    );
  }

  private deleteFileDatabase(key: string): Promise<void> {
    return this.db.list(this.basePath).remove(key);
  }

  private deleteFileStorage(name: string): void {
    const storageRef = this.storage.ref(this.basePath);
    storageRef.child(name).delete();
  }

  getUserImageURL(korisnikId: number): Observable<string> {
    return this.db.list(this.basePath, ref =>
      ref.orderByChild('korisnikId').equalTo(korisnikId)
    ).snapshotChanges().pipe(
      map(actions => {
        if (actions.length > 0) {
        const fileData : any = actions[0].payload.val();
        return fileData.url;
        } else {
          return null;
        }
      })
    );
  }
}
