import {server} from '@/tools/servers'

/**
 * 登录
 */
export class AddCompanyApi {
  // 发送验证码
  static getAuthCode(data) {
    console.log(data);
    return server.connection('POST', `api/File/SendAuthCode?email=`+data)
  }
  //添加公司
  static addCompany(data={}){
    return server.connection('POST','api/File/AddCompany',data)
  }
}