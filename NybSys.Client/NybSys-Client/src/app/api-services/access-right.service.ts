import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { ApiConstant } from '../common/Constant/APIConstant';
import { AccessRights } from '../common/Enums/Enums';
import { AccessRight } from '../Models/AccessRight';

@Injectable({
  providedIn: 'root'
})
export class AccessRightService {

  constructor(private http : HttpClient) { }

  public GetAllAccessRight() {
    return this.http.post(ApiConstant.AccessRight.GetAllAccessRight, "");
  }

  public GetAllController() {
    return this.http.post(ApiConstant.AccessRight.GetAllController, "");
  }

  public GetAllActiont() {
    return this.http.post(ApiConstant.AccessRight.GetAllAction, "");
  }

  public GetAccessRightByRole(roleName: string) {
    return this.http.post(ApiConstant.AccessRight.GetAllAccessRightByRole, roleName);
  }

  public SaveOrUpdateAccessRightByRole(accessright: AccessRight) {
    return this.http.post(ApiConstant.AccessRight.SaveOrUpdateAccessRightByRole, accessright);
  }
}