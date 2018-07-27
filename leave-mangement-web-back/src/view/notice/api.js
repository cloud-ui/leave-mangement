import {server} from '@/tools/servers'

/**
 * 公告
 */
export class NoticeApi {
  //添加公告
  static addNotice(data={}){
      return server.connection('POST','api/Notice/AddNotice',data)
  }
  //获取列表
  static noticeList(data={}){
      return server.connection('POST','api/Notice/NoticeList',data)
  }
  //删除公告
  static deleteNotice(id){
    return server.connection('Delete','api/Notice/DeleteNotice?id='+id)
  }
}