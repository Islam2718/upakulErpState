import { Injectable } from '@angular/core';
import * as CryptoJS from 'crypto-js';
import { formatDate } from '@angular/common';

@Injectable({
  providedIn: 'root',
})
export class EncryptionService {
  private secretKey = `COAST-${formatDate(new Date(),'yyyyMMdd','en-US')}`; // 🔐 Should be moved to env file in real apps

  // Encrypt data
  encryptUrlParm(data: object): string {
    const json = JSON.stringify(data);
    return CryptoJS.AES.encrypt(json, this.secretKey).toString();
  }

  // Decrypt data
  decryptUrlParm(encryptedData: string): string {
    const bytes = CryptoJS.AES.decrypt(encryptedData, this.secretKey);
    return bytes.toString(CryptoJS.enc.Utf8);
  }
}