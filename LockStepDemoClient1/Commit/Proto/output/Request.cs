// <auto-generated>
//     Generated by the protocol buffer compiler.  DO NOT EDIT!
//     source: Request.proto
// </auto-generated>
#pragma warning disable 1591, 0612, 3021, 8981
#region Designer generated code

using pb = global::Google.Protobuf;
using pbc = global::Google.Protobuf.Collections;
using pbr = global::Google.Protobuf.Reflection;
using scg = global::System.Collections.Generic;
/// <summary>Holder for reflection information generated from Request.proto</summary>
public static partial class RequestReflection {

  #region Descriptor
  /// <summary>File descriptor for Request.proto</summary>
  public static pbr::FileDescriptor Descriptor {
    get { return descriptor; }
  }
  private static pbr::FileDescriptor descriptor;

  static RequestReflection() {
    byte[] descriptorData = global::System.Convert.FromBase64String(
        string.Concat(
          "Cg1SZXF1ZXN0LnByb3RvGgpVc2VyLnByb3RvGglNc2cucHJvdG8aDFN0YXR1",
          "cy5wcm90byKuAQoLQmFzZVJlcXVlc3QSCgoCaWQYASABKAUSIQoLcmVxdWVz",
          "dFR5cGUYAiABKA4yDC5SZXF1ZXN0VHlwZRIhCgtyZXF1ZXN0RGF0YRgDIAEo",
          "DjIMLlJlcXVlc3REYXRhEhUKBHVzZXIYBCABKAsyBS5Vc2VySAASEwoDbXNn",
          "GAUgASgLMgQuTXNnSAASGQoGc3RhdHVzGAYgASgLMgcuU3RhdHVzSABCBgoE",
          "ZGF0YSo7CgtSZXF1ZXN0VHlwZRIOCgpSVF9VTktOT1dOEAASDAoIUlRfTE9H",
          "SU4QARIOCgpSVF9NRVNTQUdFEAIqSQoLUmVxdWVzdERhdGESDgoKUkRfVU5L",
          "Tk9XThAAEgsKB1JEX1VTRVIQARINCglSRF9TVEFUVVMQAhIOCgpSRF9NRVNT",
          "QUdFEANiBnByb3RvMw=="));
    descriptor = pbr::FileDescriptor.FromGeneratedCode(descriptorData,
        new pbr::FileDescriptor[] { global::UserReflection.Descriptor, global::MsgReflection.Descriptor, global::StatusReflection.Descriptor, },
        new pbr::GeneratedClrTypeInfo(new[] {typeof(global::RequestType), typeof(global::RequestData), }, null, new pbr::GeneratedClrTypeInfo[] {
          new pbr::GeneratedClrTypeInfo(typeof(global::BaseRequest), global::BaseRequest.Parser, new[]{ "Id", "RequestType", "RequestData", "User", "Msg", "Status" }, new[]{ "Data" }, null, null, null)
        }));
  }
  #endregion

}
#region Enums
/// <summary>
/// 请求的类型
/// </summary>
public enum RequestType {
  /// <summary>
  /// 未知请求
  /// </summary>
  [pbr::OriginalName("RT_UNKNOWN")] RtUnknown = 0,
  /// <summary>
  /// 登陆请求
  /// </summary>
  [pbr::OriginalName("RT_LOGIN")] RtLogin = 1,
  /// <summary>
  /// 信息
  /// </summary>
  [pbr::OriginalName("RT_MESSAGE")] RtMessage = 2,
}

/// <summary>
/// 夹带的参数
/// </summary>
public enum RequestData {
  /// <summary>
  /// 未知类型
  /// </summary>
  [pbr::OriginalName("RD_UNKNOWN")] RdUnknown = 0,
  /// <summary>
  /// 类型为User
  /// </summary>
  [pbr::OriginalName("RD_USER")] RdUser = 1,
  /// <summary>
  /// 类型为status
  /// </summary>
  [pbr::OriginalName("RD_STATUS")] RdStatus = 2,
  /// <summary>
  /// 消息
  /// </summary>
  [pbr::OriginalName("RD_MESSAGE")] RdMessage = 3,
}

#endregion

#region Messages
/// <summary>
/// 基础请求类型
/// </summary>
[global::System.Diagnostics.DebuggerDisplayAttribute("{ToString(),nq}")]
public sealed partial class BaseRequest : pb::IMessage<BaseRequest>
#if !GOOGLE_PROTOBUF_REFSTRUCT_COMPATIBILITY_MODE
    , pb::IBufferMessage
#endif
{
  private static readonly pb::MessageParser<BaseRequest> _parser = new pb::MessageParser<BaseRequest>(() => new BaseRequest());
  private pb::UnknownFieldSet _unknownFields;
  [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
  [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
  public static pb::MessageParser<BaseRequest> Parser { get { return _parser; } }

  [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
  [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
  public static pbr::MessageDescriptor Descriptor {
    get { return global::RequestReflection.Descriptor.MessageTypes[0]; }
  }

  [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
  [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
  pbr::MessageDescriptor pb::IMessage.Descriptor {
    get { return Descriptor; }
  }

  [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
  [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
  public BaseRequest() {
    OnConstruction();
  }

  partial void OnConstruction();

  [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
  [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
  public BaseRequest(BaseRequest other) : this() {
    id_ = other.id_;
    requestType_ = other.requestType_;
    requestData_ = other.requestData_;
    switch (other.DataCase) {
      case DataOneofCase.User:
        User = other.User.Clone();
        break;
      case DataOneofCase.Msg:
        Msg = other.Msg.Clone();
        break;
      case DataOneofCase.Status:
        Status = other.Status.Clone();
        break;
    }

    _unknownFields = pb::UnknownFieldSet.Clone(other._unknownFields);
  }

  [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
  [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
  public BaseRequest Clone() {
    return new BaseRequest(this);
  }

  /// <summary>Field number for the "id" field.</summary>
  public const int IdFieldNumber = 1;
  private int id_;
  /// <summary>
  /// 是谁发送的请求
  /// </summary>
  [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
  [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
  public int Id {
    get { return id_; }
    set {
      id_ = value;
    }
  }

  /// <summary>Field number for the "requestType" field.</summary>
  public const int RequestTypeFieldNumber = 2;
  private global::RequestType requestType_ = global::RequestType.RtUnknown;
  [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
  [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
  public global::RequestType RequestType {
    get { return requestType_; }
    set {
      requestType_ = value;
    }
  }

  /// <summary>Field number for the "requestData" field.</summary>
  public const int RequestDataFieldNumber = 3;
  private global::RequestData requestData_ = global::RequestData.RdUnknown;
  [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
  [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
  public global::RequestData RequestData {
    get { return requestData_; }
    set {
      requestData_ = value;
    }
  }

  /// <summary>Field number for the "user" field.</summary>
  public const int UserFieldNumber = 4;
  [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
  [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
  public global::User User {
    get { return dataCase_ == DataOneofCase.User ? (global::User) data_ : null; }
    set {
      data_ = value;
      dataCase_ = value == null ? DataOneofCase.None : DataOneofCase.User;
    }
  }

  /// <summary>Field number for the "msg" field.</summary>
  public const int MsgFieldNumber = 5;
  [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
  [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
  public global::Msg Msg {
    get { return dataCase_ == DataOneofCase.Msg ? (global::Msg) data_ : null; }
    set {
      data_ = value;
      dataCase_ = value == null ? DataOneofCase.None : DataOneofCase.Msg;
    }
  }

  /// <summary>Field number for the "status" field.</summary>
  public const int StatusFieldNumber = 6;
  [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
  [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
  public global::Status Status {
    get { return dataCase_ == DataOneofCase.Status ? (global::Status) data_ : null; }
    set {
      data_ = value;
      dataCase_ = value == null ? DataOneofCase.None : DataOneofCase.Status;
    }
  }

  private object data_;
  /// <summary>Enum of possible cases for the "data" oneof.</summary>
  public enum DataOneofCase {
    None = 0,
    User = 4,
    Msg = 5,
    Status = 6,
  }
  private DataOneofCase dataCase_ = DataOneofCase.None;
  [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
  [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
  public DataOneofCase DataCase {
    get { return dataCase_; }
  }

  [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
  [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
  public void ClearData() {
    dataCase_ = DataOneofCase.None;
    data_ = null;
  }

  [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
  [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
  public override bool Equals(object other) {
    return Equals(other as BaseRequest);
  }

  [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
  [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
  public bool Equals(BaseRequest other) {
    if (ReferenceEquals(other, null)) {
      return false;
    }
    if (ReferenceEquals(other, this)) {
      return true;
    }
    if (Id != other.Id) return false;
    if (RequestType != other.RequestType) return false;
    if (RequestData != other.RequestData) return false;
    if (!object.Equals(User, other.User)) return false;
    if (!object.Equals(Msg, other.Msg)) return false;
    if (!object.Equals(Status, other.Status)) return false;
    if (DataCase != other.DataCase) return false;
    return Equals(_unknownFields, other._unknownFields);
  }

  [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
  [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
  public override int GetHashCode() {
    int hash = 1;
    if (Id != 0) hash ^= Id.GetHashCode();
    if (RequestType != global::RequestType.RtUnknown) hash ^= RequestType.GetHashCode();
    if (RequestData != global::RequestData.RdUnknown) hash ^= RequestData.GetHashCode();
    if (dataCase_ == DataOneofCase.User) hash ^= User.GetHashCode();
    if (dataCase_ == DataOneofCase.Msg) hash ^= Msg.GetHashCode();
    if (dataCase_ == DataOneofCase.Status) hash ^= Status.GetHashCode();
    hash ^= (int) dataCase_;
    if (_unknownFields != null) {
      hash ^= _unknownFields.GetHashCode();
    }
    return hash;
  }

  [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
  [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
  public override string ToString() {
    return pb::JsonFormatter.ToDiagnosticString(this);
  }

  [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
  [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
  public void WriteTo(pb::CodedOutputStream output) {
  #if !GOOGLE_PROTOBUF_REFSTRUCT_COMPATIBILITY_MODE
    output.WriteRawMessage(this);
  #else
    if (Id != 0) {
      output.WriteRawTag(8);
      output.WriteInt32(Id);
    }
    if (RequestType != global::RequestType.RtUnknown) {
      output.WriteRawTag(16);
      output.WriteEnum((int) RequestType);
    }
    if (RequestData != global::RequestData.RdUnknown) {
      output.WriteRawTag(24);
      output.WriteEnum((int) RequestData);
    }
    if (dataCase_ == DataOneofCase.User) {
      output.WriteRawTag(34);
      output.WriteMessage(User);
    }
    if (dataCase_ == DataOneofCase.Msg) {
      output.WriteRawTag(42);
      output.WriteMessage(Msg);
    }
    if (dataCase_ == DataOneofCase.Status) {
      output.WriteRawTag(50);
      output.WriteMessage(Status);
    }
    if (_unknownFields != null) {
      _unknownFields.WriteTo(output);
    }
  #endif
  }

  #if !GOOGLE_PROTOBUF_REFSTRUCT_COMPATIBILITY_MODE
  [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
  [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
  void pb::IBufferMessage.InternalWriteTo(ref pb::WriteContext output) {
    if (Id != 0) {
      output.WriteRawTag(8);
      output.WriteInt32(Id);
    }
    if (RequestType != global::RequestType.RtUnknown) {
      output.WriteRawTag(16);
      output.WriteEnum((int) RequestType);
    }
    if (RequestData != global::RequestData.RdUnknown) {
      output.WriteRawTag(24);
      output.WriteEnum((int) RequestData);
    }
    if (dataCase_ == DataOneofCase.User) {
      output.WriteRawTag(34);
      output.WriteMessage(User);
    }
    if (dataCase_ == DataOneofCase.Msg) {
      output.WriteRawTag(42);
      output.WriteMessage(Msg);
    }
    if (dataCase_ == DataOneofCase.Status) {
      output.WriteRawTag(50);
      output.WriteMessage(Status);
    }
    if (_unknownFields != null) {
      _unknownFields.WriteTo(ref output);
    }
  }
  #endif

  [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
  [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
  public int CalculateSize() {
    int size = 0;
    if (Id != 0) {
      size += 1 + pb::CodedOutputStream.ComputeInt32Size(Id);
    }
    if (RequestType != global::RequestType.RtUnknown) {
      size += 1 + pb::CodedOutputStream.ComputeEnumSize((int) RequestType);
    }
    if (RequestData != global::RequestData.RdUnknown) {
      size += 1 + pb::CodedOutputStream.ComputeEnumSize((int) RequestData);
    }
    if (dataCase_ == DataOneofCase.User) {
      size += 1 + pb::CodedOutputStream.ComputeMessageSize(User);
    }
    if (dataCase_ == DataOneofCase.Msg) {
      size += 1 + pb::CodedOutputStream.ComputeMessageSize(Msg);
    }
    if (dataCase_ == DataOneofCase.Status) {
      size += 1 + pb::CodedOutputStream.ComputeMessageSize(Status);
    }
    if (_unknownFields != null) {
      size += _unknownFields.CalculateSize();
    }
    return size;
  }

  [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
  [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
  public void MergeFrom(BaseRequest other) {
    if (other == null) {
      return;
    }
    if (other.Id != 0) {
      Id = other.Id;
    }
    if (other.RequestType != global::RequestType.RtUnknown) {
      RequestType = other.RequestType;
    }
    if (other.RequestData != global::RequestData.RdUnknown) {
      RequestData = other.RequestData;
    }
    switch (other.DataCase) {
      case DataOneofCase.User:
        if (User == null) {
          User = new global::User();
        }
        User.MergeFrom(other.User);
        break;
      case DataOneofCase.Msg:
        if (Msg == null) {
          Msg = new global::Msg();
        }
        Msg.MergeFrom(other.Msg);
        break;
      case DataOneofCase.Status:
        if (Status == null) {
          Status = new global::Status();
        }
        Status.MergeFrom(other.Status);
        break;
    }

    _unknownFields = pb::UnknownFieldSet.MergeFrom(_unknownFields, other._unknownFields);
  }

  [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
  [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
  public void MergeFrom(pb::CodedInputStream input) {
  #if !GOOGLE_PROTOBUF_REFSTRUCT_COMPATIBILITY_MODE
    input.ReadRawMessage(this);
  #else
    uint tag;
    while ((tag = input.ReadTag()) != 0) {
    if ((tag & 7) == 4) {
      // Abort on any end group tag.
      return;
    }
    switch(tag) {
        default:
          _unknownFields = pb::UnknownFieldSet.MergeFieldFrom(_unknownFields, input);
          break;
        case 8: {
          Id = input.ReadInt32();
          break;
        }
        case 16: {
          RequestType = (global::RequestType) input.ReadEnum();
          break;
        }
        case 24: {
          RequestData = (global::RequestData) input.ReadEnum();
          break;
        }
        case 34: {
          global::User subBuilder = new global::User();
          if (dataCase_ == DataOneofCase.User) {
            subBuilder.MergeFrom(User);
          }
          input.ReadMessage(subBuilder);
          User = subBuilder;
          break;
        }
        case 42: {
          global::Msg subBuilder = new global::Msg();
          if (dataCase_ == DataOneofCase.Msg) {
            subBuilder.MergeFrom(Msg);
          }
          input.ReadMessage(subBuilder);
          Msg = subBuilder;
          break;
        }
        case 50: {
          global::Status subBuilder = new global::Status();
          if (dataCase_ == DataOneofCase.Status) {
            subBuilder.MergeFrom(Status);
          }
          input.ReadMessage(subBuilder);
          Status = subBuilder;
          break;
        }
      }
    }
  #endif
  }

  #if !GOOGLE_PROTOBUF_REFSTRUCT_COMPATIBILITY_MODE
  [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
  [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
  void pb::IBufferMessage.InternalMergeFrom(ref pb::ParseContext input) {
    uint tag;
    while ((tag = input.ReadTag()) != 0) {
    if ((tag & 7) == 4) {
      // Abort on any end group tag.
      return;
    }
    switch(tag) {
        default:
          _unknownFields = pb::UnknownFieldSet.MergeFieldFrom(_unknownFields, ref input);
          break;
        case 8: {
          Id = input.ReadInt32();
          break;
        }
        case 16: {
          RequestType = (global::RequestType) input.ReadEnum();
          break;
        }
        case 24: {
          RequestData = (global::RequestData) input.ReadEnum();
          break;
        }
        case 34: {
          global::User subBuilder = new global::User();
          if (dataCase_ == DataOneofCase.User) {
            subBuilder.MergeFrom(User);
          }
          input.ReadMessage(subBuilder);
          User = subBuilder;
          break;
        }
        case 42: {
          global::Msg subBuilder = new global::Msg();
          if (dataCase_ == DataOneofCase.Msg) {
            subBuilder.MergeFrom(Msg);
          }
          input.ReadMessage(subBuilder);
          Msg = subBuilder;
          break;
        }
        case 50: {
          global::Status subBuilder = new global::Status();
          if (dataCase_ == DataOneofCase.Status) {
            subBuilder.MergeFrom(Status);
          }
          input.ReadMessage(subBuilder);
          Status = subBuilder;
          break;
        }
      }
    }
  }
  #endif

}

#endregion


#endregion Designer generated code
