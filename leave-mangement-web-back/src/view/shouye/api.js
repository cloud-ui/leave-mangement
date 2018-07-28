import {server} from '@/tools/servers'

/**
 * 公告
 */
export class ShouyeApi {
  //获取列表
  static noticeList(data={}){
      return server.connection('POST','api/Notice/NoticeList',data)
  }
}